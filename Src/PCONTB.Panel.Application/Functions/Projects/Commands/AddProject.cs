using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Commands
{
    public class AddProjectRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid? CategoryId { get; set; }
        public Guid? CountryId { get; set; }

        public FormFile? Image { get; set; }
        public byte[] ImageData { get; set; }
        public FormFile? Video { get; set; }
        public byte[] VideoData { get; set; }
        public List<AddProjectCollaboratorDto> Collaborators { get; set; } = new List<AddProjectCollaboratorDto>();

    }

    public class AddProjectHandler(
        IUnitOfWork unitOfWork, 
        ISessionAccesor sessionAccesor, 
        IProjectFileService projectFileService, 
        IProjectCollaboratorService projectCollabortatorService) 
        : IRequestHandler<AddProjectRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(AddProjectRequest request, CancellationToken cancellationToken)
        {
            var userId = sessionAccesor.Session.UserId;

            var aggregate = new Project(Guid.NewGuid(), request.Name, userId, (Guid)request.CountryId, (Guid)request.CategoryId);

            await projectFileService.UploadImage(aggregate, request.Image, cancellationToken);

            await projectFileService.UploadVideo(aggregate, request.Video, cancellationToken);

            await projectCollabortatorService.AddCollaborators(aggregate, request.Collaborators, cancellationToken);

            await unitOfWork.ProjectRepository.Add(aggregate, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(aggregate.Id);
        }
    }

    public class AddProjectRequestValidator : AbstractValidator<AddProjectRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private static readonly string[] AllowedImageContentTypes =
        [
            "image/jpeg",
            "image/png",
            "image/webp",
            "image/gif"
        ];

        private static readonly string[] AllowedVideoContentTypes =
        [
            "video/mp4",
            "video/webm",
            "video/ogg",
            "video/quicktime"
        ];

        public AddProjectRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ErrorCodes.Projects.Project.ProjectNameEmpty.Message);

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage(ErrorCodes.Projects.Project.CategoryEmpty.Message)
                .MustAsync(CategoryExist).WithMessage(ErrorCodes.Projects.Project.CategoryExist.Message);
                
            RuleFor(p => p.CountryId)
                .NotEmpty().WithMessage(ErrorCodes.Projects.Project.CountryEmpty.Message)
                .MustAsync(ConuntryExit).WithMessage(ErrorCodes.Projects.Project.CountryExist.Message);

            RuleFor(x => x.Image).ChildRules(images =>
            {
                images.RuleFor(f => f.ContentType)
                    .Must(ct => AllowedImageContentTypes.Contains(ct))
                    .WithMessage(ErrorCodes.Projects.ProjectImage.TypeAllowed.Message);
            });

            RuleFor(x => x.Video).ChildRules(videos =>
            {
                videos.RuleFor(f => f.ContentType)
                    .Must(ct => AllowedVideoContentTypes.Contains(ct))
                    .WithMessage(ErrorCodes.Projects.ProjectVideo.TypeAllowed.Message);
            });

            RuleFor(x => x.Collaborators).Custom((list, context) =>
            {
                var duplicateEmails = list
                    .GroupBy(c => c.Email?.Trim().ToLower())
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                foreach (var email in duplicateEmails)
                {
                    var indexes = list
                        .Select((item, index) => new { item, index })
                        .Where(x => x.item.Email?.Trim().ToLower() == email)
                        .Select(x => x.index);

                    foreach (var i in indexes)
                    {
                        context.AddFailure($"Collaborators[{i}].Email", ErrorCodes.Projects.ProjectCollaborator.UserExistInProject.Message);
                    }
                }
            });

            RuleFor(x => x.Collaborators).ForEach(collaborator =>
            {
                collaborator.ChildRules(c =>
                {
                    c.RuleFor(colab => colab.Email)
                        .NotEmpty().WithMessage(ErrorCodes.Projects.ProjectCollaborator.UserEmpty.Message)
                        .MustAsync(UserExist).WithMessage(ErrorCodes.Projects.ProjectCollaborator.UserExist.Message)
                        .WithMessage(ErrorCodes.Projects.ProjectCollaborator.UserExistInProject.Message);
                });
            });
        }

        private async Task<bool> CategoryExist(Guid? id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CategoryRepository.Exist(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> ConuntryExit(Guid? id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CountryRepository.Exist(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> UserExist(string email, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.Exist(m => m.Email == email, cancellationToken);
        }
    }
}
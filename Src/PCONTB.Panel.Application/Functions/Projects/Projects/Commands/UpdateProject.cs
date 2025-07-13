using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Files;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands.Models;
using PCONTB.Panel.Domain.Projects.Projects.Files;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class UpdateProjectRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid CountryId { get; set; }
        public FormFile Image { get; set; }
        public byte[] ImageData { get; set; }
        public FormFile? Video { get; set; }
        public byte[] VideoData { get; set; }
        public List<ProjectCollaboratorDto> Collaborators { get; set; } = new List<ProjectCollaboratorDto>();

    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, CommandResult> 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;

        public UpdateProjectHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor)
        {
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await _unitOfWork.ProjectRepository.GetByTracking(m => m.Id == request.Id, cancellationToken);

            if (aggregate == null) throw new NotFoundException("Project not found");

            _sessionAccesor.Verify(aggregate.UserId);

            aggregate.SetName(request.Name);
            aggregate.SetCountry(request.CountryId);
            aggregate.SetCategory(request.CategoryId);
            aggregate.SetSubcategory(request.SubcategoryId);

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(request.Image.Path))
                {
                    var data = await File.ReadAllBytesAsync(request.Image.Path, cancellationToken);

                    if (aggregate.Image == null)
                    {
                        aggregate.SetImage(new ProjectImage(request.Image.FileName, request.Image.ContentType, data));
                    }
                    else
                    {
                        aggregate.Image.SetFileName(request.Image.FileName);
                        aggregate.Image.SetImageData(data);
                        aggregate.Image.SetContentType(request.Image.ContentType);
                    }
                }
            }

            var collaborators = request.Collaborators.Select(m => ProjectCollaboratorDto.Map(m, aggregate.Id)).ToList();

            aggregate.SetCollaborators(collaborators);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(aggregate.Id);
        }
    }

    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private static readonly string[] AllowedContentTypes =
        [
            "image/jpeg",
            "image/png",
            "image/webp",
            "image/gif"
        ];

        public UpdateProjectRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ErrorCodes.Project.ProjectNameEmpty.Message);

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage(ErrorCodes.Project.CategoryEmpty.Message)
                .MustAsync(CategoryExist).WithMessage(ErrorCodes.Project.CategoryExist.Message);

            RuleFor(p => p.CountryId)
                .NotEmpty().WithMessage(ErrorCodes.Project.CountryEmpty.Message)
                .MustAsync(ConuntryExit).WithMessage(ErrorCodes.Project.CountryExist.Message);

            RuleFor(x => x.Image).ChildRules(images =>
            {
                //images.RuleFor(f => f.Length)
                //    .LessThanOrEqualTo(MaxFileSizeInBytes)
                //    .WithMessage("Each image must be smaller than 5 MB.");

                images.RuleFor(f => f.ContentType)
                    .Must(ct => AllowedContentTypes.Contains(ct))
                    .WithMessage("Only JPG, PNG, WEBP or GIF files are allowed.");
            });

            RuleFor(x => x.Collaborators).ForEach(collaborator =>
            {
                collaborator.ChildRules(c =>
                {
                    c.RuleFor(colab => colab.Email)
                        .NotEmpty().WithMessage(ErrorCodes.Collaborator.UserEmpty.Message)
                        .MustAsync(UserExist).WithMessage(ErrorCodes.Collaborator.UserExist.Message)
                        .MustAsync(async (s, u, ct) => await UserExistInProject(s.Id, s.ProjectId, u, ct))
                        .WithMessage(ErrorCodes.Collaborator.UserExistInProject.Message);

                    c.RuleFor(colab => colab.ProjectId)
                        .NotEmpty().WithMessage(ErrorCodes.Collaborator.ProjectEmpty.Message)
                        .MustAsync(ProjectExist).WithMessage(ErrorCodes.Collaborator.ProjectExist.Message);
                });
            });
        }

        private async Task<bool> CategoryExist(Guid id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CategoryRepository.Exist(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> ConuntryExit(Guid id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CountryRepository.Exist(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> UserExist(string email, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.Exist(m => m.Email == email, cancellationToken);
        }

        private async Task<bool> UserExistInProject(Guid id, Guid projectId, string email, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == projectId, cancellationToken);

            return !project.Collaborators.Any(m => m.User.Email == email && m.Id != id);
        }

        private async Task<bool> ProjectExist(Guid id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProjectRepository.Exist(m => m.Id == id, cancellationToken);
        }
    }
}

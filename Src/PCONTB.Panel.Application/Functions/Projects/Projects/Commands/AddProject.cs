using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Files;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands.Models;
using PCONTB.Panel.Domain.Projects.Projects;
using PCONTB.Panel.Domain.Projects.Projects.Files;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class AddProjectRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid? CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid? CountryId { get; set; }

        public FormFile? Image { get; set; }
        public byte[] ImageData { get; set; }
        public FormFile? Video { get; set; }
        public byte[] VideoData { get; set; }
        public List<ProjectCollaboratorDto> Collaborators { get; set; } = new List<ProjectCollaboratorDto>();

    }

    public class AddProjectHandler : IRequestHandler<AddProjectRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;

        public AddProjectHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor)
        {
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(AddProjectRequest request, CancellationToken cancellationToken)
        {
            var userId = _sessionAccesor.Session.UserId;

            var aggregate = new Project(Guid.NewGuid(), request.Name, userId, (Guid)request.CountryId, (Guid)request.CategoryId, request.SubcategoryId);

            if (request.Image is not null)
            {
                var data = await File.ReadAllBytesAsync(request.Image.Path, cancellationToken);

                var entity = new ProjectImage(request.Image.FileName, request.Image.ContentType, data);

                aggregate.SetImage(entity);
            }

            var collaborators = request.Collaborators.Select(m => ProjectCollaboratorDto.Map(m, aggregate.Id)).ToList();

            aggregate.SetCollaborators(collaborators);

            await _unitOfWork.ProjectRepository.Add(aggregate, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(aggregate.Id);
        }
    }

    public class AddProjectRequestValidator : AbstractValidator<AddProjectRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private static readonly string[] AllowedContentTypes =
        [
            "image/jpeg",
            "image/png",
            "image/webp",
            "image/gif"
        ];

        public AddProjectRequestValidator(IUnitOfWork unitOfWork)
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
                        context.AddFailure($"Collaborators[{i}].Email", $"Duplicate email: {email}");
                    }
                }
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
    }
}
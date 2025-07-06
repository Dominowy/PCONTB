using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Files;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Models.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects.Files;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Projects.Commands
{
    public class UpdateProjectRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid CountryId { get; set; }
        public List<CollaboratorDto> Collaborators { get; set; }
        public FormFile Image { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? VideoId { get; set; }
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
            var entity = await _unitOfWork.ProjectRepository.GetByTracking(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("Project not found");

            _sessionAccesor.Verify(entity.UserId);

            entity.SetName(request.Name);
            entity.SetCountry(request.CountryId);
            entity.SetCategory(request.CategoryId);
            entity.SetSubcategory(request.SubcategoryId);

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(request.Image.Path))
                {
                    var data = await File.ReadAllBytesAsync(request.Image.Path, cancellationToken);

                    if (entity.Image == null)
                    {
                        entity.SetImage(new ProjectImage(request.Image.FileName, request.Image.ContentType, data));
                    }
                    else
                    {
                        entity.Image.SetFileName(request.Image.FileName);
                        entity.Image.SetImageData(data);
                        entity.Image.SetContentType(request.Image.ContentType);
                    }
                }
            }

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
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
        }

        private async Task<bool> CategoryExist(Guid id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CategoryRepository.Exist(m => m.Id == id, cancellationToken);
        }

        private async Task<bool> ConuntryExit(Guid id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CountryRepository.Exist(m => m.Id == id, cancellationToken);
        }
    }
}

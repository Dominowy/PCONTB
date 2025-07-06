using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Commands
{
    public class UpdateProjectCollaboratorRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Guid ProjectId { get; set; }
        public bool ManageProjectPermission { get; set; } = false;
        public bool ManageCommunityPermission { get; set; } = false;
        public bool ManageFulfillmentPermission { get; set; } = false;
    }

    public class UpdateCollaboratorHandler : IRequestHandler<UpdateProjectCollaboratorRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCollaboratorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(UpdateProjectCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == request.ProjectId, cancellationToken);

            var entity = project.Collaborators.FirstOrDefault(m => m.Id == request.ProjectId);

            if (entity is null) throw new NotFoundException(ErrorCodes.Collaborator.NotFound.Message);

            entity.SetManageProjectPermission(request.ManageProjectPermission);
            entity.SetManageCommunityPermission(request.ManageCommunityPermission);
            entity.SetManageFulfillmentPermission(request.ManageFulfillmentPermission);

            await _unitOfWork.ProjectRepository.Update(project, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateProjectCollaboratorRequestValidator : AbstractValidator<UpdateProjectCollaboratorRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProjectCollaboratorRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.UserEmpty.Message)
                .MustAsync(UserExist).WithMessage(ErrorCodes.Collaborator.UserExist.Message)
                .MustAsync(async (s, u, ct) => await UserExistInProject(s.Id, s.ProjectId, u, ct))
                .WithMessage(ErrorCodes.Collaborator.UserExistInProject.Message);

            RuleFor(p => p.ProjectId)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.ProjectEmpty.Message)
                .MustAsync(ProjectExist).WithMessage(ErrorCodes.Collaborator.ProjectExist.Message);
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

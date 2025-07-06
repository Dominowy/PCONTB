using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Domain.Projects.Projects.Collaborators;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Commands
{
    public class AddProjectCollaboratorRequest : BaseCommand, IRequest<CommandResult>
    {
        public string Email { get; set; }
        public Guid ProjectId { get; set; }

        public bool ManageProjectPermission { get; set; } = false;
        public bool ManageCommunityPermission { get; set; } = false;
        public bool ManageFulfillmentPermission { get; set; } = false;
    }

    public class AddProjectCollaboratorHandler : IRequestHandler<AddProjectCollaboratorRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddProjectCollaboratorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(AddProjectCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetBy(m => m.Email == request.Email, cancellationToken);

            if (user is null) throw new NotFoundException(ErrorCodes.Collaborator.UserExist.Message);

            var project = await _unitOfWork.ProjectRepository.GetByTracking(m => m.Id == request.ProjectId, cancellationToken);

            var entity = new ProjectCollaborator(user.Id, request.ProjectId);

            entity.SetManageProjectPermission(request.ManageProjectPermission);
            entity.SetManageCommunityPermission(request.ManageCommunityPermission);
            entity.SetManageFulfillmentPermission(request.ManageFulfillmentPermission);

            project.AddCollaborator(entity);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddProjectCollaboratorRequestValidator : AbstractValidator<AddProjectCollaboratorRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddProjectCollaboratorRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.UserEmpty.Message)
                .MustAsync(UserExist).WithMessage(ErrorCodes.Collaborator.UserExist.Message)
                .MustAsync(async (s, u, ct) => await UserExistInProject(s.ProjectId, u, ct))
                .WithMessage(ErrorCodes.Collaborator.UserExistInProject.Message);

            RuleFor(p => p.ProjectId)
                .NotEmpty().WithMessage(ErrorCodes.Collaborator.ProjectEmpty.Message)
                .MustAsync(ProjectExist).WithMessage(ErrorCodes.Collaborator.ProjectExist.Message);
        }

        private async Task<bool> UserExist(string email, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.Exist(m => m.Email == email, cancellationToken);
        }

        private async Task<bool> UserExistInProject(Guid projectId, string email, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == projectId, cancellationToken);

            return !project.Collaborators.Any(m => m.User.Email == email);
        }

        private async Task<bool> ProjectExist(Guid id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProjectRepository.Exist(m => m.Id == id, cancellationToken);
        }
    }
}

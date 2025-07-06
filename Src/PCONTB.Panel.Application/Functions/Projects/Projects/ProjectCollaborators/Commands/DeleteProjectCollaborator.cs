using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Commands
{
    public class DeleteProjectCollaboratorRequest : BaseCommand, IRequest<CommandResult>
    {
        public Guid ProjectId { get; set; }
    }

    public class DeleteCollaboratorHandler : IRequestHandler<DeleteProjectCollaboratorRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCollaboratorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(DeleteProjectCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetByTracking(m => m.Id == request.ProjectId, cancellationToken);

            var entity = project.Collaborators.FirstOrDefault(c => c.Id == request.Id);

            if (entity is null) throw new NotFoundException(ErrorCodes.Collaborator.NotFound.Message);

            project.Collaborators.Remove(entity);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

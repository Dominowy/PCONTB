using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Commands
{
    public class DeleteProjectRequest : BaseCommand, IRequest<CommandResult>
    {

    }

    public class DeleteProjectHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor) 
        : IRequestHandler<DeleteProjectRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Project not found");

            sessionAccesor.Verify(entity.UserId);

            await unitOfWork.ProjectRepository.Delete(entity, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Functions.Projects.Collaborators.Commands
{
    public class DeleteCollaboratorRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class DeleteCollaboratorHandler : IRequestHandler<DeleteCollaboratorRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteCollaboratorHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(DeleteCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Collaborator>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Collaborator.NotFound.Message);

            _dbContext.Set<Collaborator>().Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

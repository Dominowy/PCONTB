using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class DeleteProjectRequest : BaseCommand, IRequest<CommandResult>
    {

    }

    public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, CommandResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISessionAccesor _sessionAccesor;

        public DeleteProjectHandler(IApplicationDbContext context, ISessionAccesor sessionAccesor)
        {
            _context = context;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("Project not found");

            _sessionAccesor.Verify(entity.UserId);

            _context.Set<Project>().Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

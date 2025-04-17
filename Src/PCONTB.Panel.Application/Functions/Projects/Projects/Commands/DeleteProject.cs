using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class DeleteProjectRequest : IRequest<CommandResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, CommandResult>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProjectHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>().FindAsync(request.Id, cancellationToken);

            if (entity == null) 
            {
                throw new NotFoundException("Project not found");
            }

            _context.Set<Project>().Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

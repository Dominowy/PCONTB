using MediatR;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class DeleteProjectRequest : IRequest<DeleteResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, DeleteResult>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProjectHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteResult> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Project>().FindAsync(request.Id, cancellationToken);

            if (entity == null) 
            {
                return new DeleteResult(false, ResponseStatus.NotFound);
            }

            _context.Set<Project>().Remove(entity);

            await _context.SaveChangesAsync();

            return new DeleteResult();
        }
    }
}

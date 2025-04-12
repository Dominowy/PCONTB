using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Dto.Projects.Projects;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetProjectRequest : IRequest<GetProjectResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetProjectHandler : IRequestHandler<GetProjectRequest, GetProjectResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetProjectHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetProjectResponse> Handle(GetProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Project>()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("Project not found");

            return new GetProjectResponse()
            {
                Project = ProjectDto.Map(entity)
            };
        }
    }

    public class GetProjectResponse
    {
        public ProjectDto Project { get; set; }
    }
}

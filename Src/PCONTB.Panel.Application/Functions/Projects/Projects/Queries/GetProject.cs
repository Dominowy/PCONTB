using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Projects.Projects;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetProjectRequest : BaseQuery, IRequest<GetProjectResponse>
    {

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
                .Include(p => p.Country)
                .Include(p => p.Collaborators).ThenInclude(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Image)
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
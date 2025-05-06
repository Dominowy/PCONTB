using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Projects.Projects;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetAllProjectsRequest : IRequest<GetAllProjectsResponse>
    {
    }

    public class GetAllByUserIdProjectsRequest : BaseQuery, IRequest<GetAllProjectsResponse>
    {
    }

    

    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsRequest, GetAllProjectsResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllProjectsHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllProjectsResponse> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Project>()
                .Include(p => p.User)
                .Include(p => p.Collaborators)
                .Include(p => p.Country)
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Images)
                .ToListAsync(cancellationToken);

            return new GetAllProjectsResponse()
            {
                Projects = [.. entity.Select(ProjectDto.Map)],
            };
        }
    }

    public class GetAllByUserIdProjectsHandler : IRequestHandler<GetAllByUserIdProjectsRequest, GetAllProjectsResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllByUserIdProjectsHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllProjectsResponse> Handle(GetAllByUserIdProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Project>()
                .Include(p => p.User)
                .Include(p => p.Collaborators)
                .Include(p => p.Country)
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Images)
                .Where(p => p.UserId == request.Id || p.Collaborators.Any(m => m.UserId == request.Id))
                .ToListAsync(cancellationToken);

            return new GetAllProjectsResponse()
            {
                Projects = [.. entity.Select(ProjectDto.Map)],
            };
        }
    }


    public class GetAllProjectsResponse
    {
        public List<ProjectDto> Projects { get; set; }
    }
}
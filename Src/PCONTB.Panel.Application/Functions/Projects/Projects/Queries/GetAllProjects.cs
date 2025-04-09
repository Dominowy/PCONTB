using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Dto.Projects.Projects;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetAllProjectsRequest : IRequest<GetAllProjectsResponse>
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
                .ToListAsync(cancellationToken);

            if (entity == null)
            {
                return new GetAllProjectsResponse(false, ResponseStatus.NotFound);
            }

            return new GetAllProjectsResponse()
            {
                Projects = entity.Select(ProjectDto.Map).ToList(),
            };
        }
    }

    public class GetAllProjectsResponse : BaseResponse
    {
        public List<ProjectDto> Projects { get; set; }

        public GetAllProjectsResponse(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {

        }

        public GetAllProjectsResponse() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.OK;
        }
    }
}

using MediatR;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetAllProjectsRequest : IRequest<GetAllProjectsResponse>
    {
    }

    public class GetAllByUserIdProjectsRequest : BaseQuery, IRequest<GetAllProjectsResponse>
    {
    }

    public class GetMostViewedProjectsRequest : BaseQuery, IRequest<GetAllProjectsResponse>
    {
    }

    public class GetLastCreatedProjectsRequest : BaseQuery, IRequest<GetAllProjectsResponse>
    {
    }

    public class GetAllProjectsHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<GetAllProjectsRequest, GetAllProjectsResponse>,
        IRequestHandler<GetAllByUserIdProjectsRequest, GetAllProjectsResponse>,
        IRequestHandler<GetMostViewedProjectsRequest, GetAllProjectsResponse>,
        IRequestHandler<GetLastCreatedProjectsRequest, GetAllProjectsResponse>
    {

        public async Task<GetAllProjectsResponse> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ProjectRepository.GetAll(cancellationToken);

            return new GetAllProjectsResponse()
            {
                Projects = [.. entity.Select(ProjectDto.Map)],
            };
        }

        public async Task<GetAllProjectsResponse> Handle(GetAllByUserIdProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ProjectRepository.GetAll(p => p.UserId == request.Id || p.Collaborators.Any(m => m.UserId == request.Id), cancellationToken);

            return new GetAllProjectsResponse()
            {
                Projects = [.. entity.Select(ProjectDto.Map)],
            };
        }

        public async Task<GetAllProjectsResponse> Handle(GetMostViewedProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.ProjectRepository.GetQuery().OrderByDescending(m => m.Views).Take(10);

            return new GetAllProjectsResponse()
            {
                Projects = [.. entity.Select(ProjectDto.Map)],
            };
        }

        public async Task<GetAllProjectsResponse> Handle(GetLastCreatedProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.ProjectRepository.GetQuery().OrderByDescending(m => m.Created).Take(10);

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
using MediatR;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Models.Projects.Projects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetAllProjectsRequest : IRequest<GetAllProjectsResponse>
    {
    }

    public class GetAllByUserIdProjectsRequest : BaseQuery, IRequest<GetAllProjectsResponse>
    {
    }

    

    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsRequest, GetAllProjectsResponse>,
        IRequestHandler<GetAllByUserIdProjectsRequest, GetAllProjectsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProjectsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllProjectsResponse> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.ProjectRepository.GetAll(cancellationToken);

            return new GetAllProjectsResponse()
            {
                Projects = [.. entity.Select(ProjectDto.Map)],
            };
        }

        public async Task<GetAllProjectsResponse> Handle(GetAllByUserIdProjectsRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.ProjectRepository.GetAll(p => p.UserId == request.Id || p.Collaborators.Any(m => m.UserId == request.Id), cancellationToken);

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
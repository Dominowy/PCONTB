using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Models.Projects.Projects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetProjectRequest : BaseQuery, IRequest<GetProjectResponse>
    {

    }

    public class GetProjectHandler : IRequestHandler<GetProjectRequest, GetProjectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetProjectResponse> Handle(GetProjectRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken);

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
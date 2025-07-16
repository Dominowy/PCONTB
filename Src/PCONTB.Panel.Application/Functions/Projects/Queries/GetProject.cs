using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetProjectRequest : BaseQuery, IRequest<GetProjectResponse>
    {

    }

    public class GetProjectHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProjectRequest, GetProjectResponse>
    {

        public async Task<GetProjectResponse> Handle(GetProjectRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException("Project not found");

            var result = ProjectDto.Map(aggregate);

            if (aggregate.Image != null)
            {
                result.Image = new FormFile
                {
                    ContentType = aggregate.Image.ContentType,
                    FileName = aggregate.Image.FileName,
                };
                result.ImageData = aggregate.Image.Data;
            }

            if (aggregate.Video != null)
            {
                result.Video = new FormFile
                {
                    ContentType = aggregate.Video.ContentType,
                    FileName = aggregate.Video.FileName,
                };
                result.VideoData = aggregate.Video.Data;
            }

            return new GetProjectResponse()
            {
                Project = result
            };
        }
    }

    public class GetProjectResponse
    {
        public ProjectDto Project { get; set; }
    }
}
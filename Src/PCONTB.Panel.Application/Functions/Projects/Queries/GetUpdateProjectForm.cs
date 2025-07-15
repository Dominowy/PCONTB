using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Application.Functions.Projects.Commands;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetUpdateProjectFormRequest : BaseQuery, IRequest<GetUpdateProjectFormResponse>
    {

    }

    public class GetUpdateProjectFormHandler(
        IUnitOfWork unitOfWork, 
        IProjectCollaboratorPermissionService permissionService) 
        : IRequestHandler<GetUpdateProjectFormRequest, GetUpdateProjectFormResponse>
    {
        public async Task<GetUpdateProjectFormResponse> Handle(GetUpdateProjectFormRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException("Project not found");

            await permissionService.Verify(aggregate, ProjectPermission.ManageProjectPermission, cancellationToken);

            FormFile image = null;

            if (aggregate.Image != null)
            {
                image = new FormFile
                {
                    ContentType = aggregate.Image.ContentType,
                    FileName = aggregate.Image.FileName,
                };
            }

            return new GetUpdateProjectFormResponse()
            {
                Form = new UpdateProjectRequest
                {
                    Id = aggregate.Id,
                    Name = aggregate.Name,
                    CategoryId = aggregate.CategoryId,
                    CountryId = aggregate.CountryId,
                    Image = image,
                    ImageData = aggregate.Image?.Data,
                    Collaborators = [.. aggregate.Collaborators.Select(UpdateProjectCollaboratorDto.Map)]
                }
            };
        }
    }

    public class GetUpdateProjectFormResponse
    {
        public UpdateProjectRequest Form{ get; set; }
    }

}

using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Extensions.Helpers.Enums;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Application.Functions.Projects.Commands;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects.Campaigns;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetUpdateProjectFormRequest : BaseQuery, IRequest<GetUpdateProjectFormResponse>
    {

    }

    public class GetUpdateProjectFormHandler(
        IUnitOfWork unitOfWork, 
        IProjectCollaboratorPermissionService permissionService,
        IProjectCampaignService projectCampaignService,
        ISessionAccesor sessionAccesor) 
        : IRequestHandler<GetUpdateProjectFormRequest, GetUpdateProjectFormResponse>
    {
        public async Task<GetUpdateProjectFormResponse> Handle(GetUpdateProjectFormRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await unitOfWork.ProjectRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException("Project not found");

            var currentCollaborator = await permissionService.GetCurrentCollaborator(aggregate, cancellationToken);

            var currentUser = new ProjectUserPermissionDto();

            if (currentCollaborator == null)
            {
                if (sessionAccesor.Session.User.Id == aggregate.User.Id)
                {
                    currentUser.ManageProjectPermission = true;
                    currentUser.ManageCommunityPermission = true;
                    currentUser.ManageFulfillmentPermission = true;
                }
            } 
            else
            {
                currentUser = ProjectUserPermissionDto.Map(currentCollaborator);
            }

            FormFile image = null;

            if (aggregate.Image != null)
            {
                image = new FormFile
                {
                    ContentType = aggregate.Image.ContentType,
                    FileName = aggregate.Image.FileName,
                };
            }

            FormFile video = null;

            if (aggregate.Video != null)
            {
                video = new FormFile
                {
                    ContentType = aggregate.Video.ContentType,
                    FileName = aggregate.Video.FileName,
                };
            }

            var contents = await projectCampaignService.GetCampaign(aggregate, cancellationToken);

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
                    Video = video,
                    VideoData = aggregate.Video?.Data,
                    Collaborators = [.. aggregate.Collaborators.Select(UpdateProjectCollaboratorDto.Map)],
                    Campaign = contents,
                    Description = aggregate.Description,
                    Views = aggregate.Views,
                },
                CurrentUser = currentUser,
                ProjectCampaignContentType = EnumHelper.EnumToList<ProjectCampaignContentType>()
            };
        }
    }

    public class GetUpdateProjectFormResponse
    {
        public UpdateProjectRequest Form{ get; set; }
        public ProjectUserPermissionDto? CurrentUser{ get; set; }
        public List<EnumItem> ProjectCampaignContentType { get; set; }  = [];

    }

}

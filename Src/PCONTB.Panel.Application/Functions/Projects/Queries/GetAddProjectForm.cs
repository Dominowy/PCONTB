using MediatR;
using PCONTB.Panel.Application.Common.Extensions.Helpers.Enums;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Projects.Commands;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects.Campaigns;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
{
    public class GetAddProjectFormRequest : IRequest<GetAddProjectFormResponse>
    {
    }

    public class GetAddProjectFormHandler(ISessionAccesor sessionAccesor) : IRequestHandler<GetAddProjectFormRequest, GetAddProjectFormResponse>
    {
        public async Task<GetAddProjectFormResponse> Handle(GetAddProjectFormRequest request, CancellationToken cancellationToken)
        {
            var currentUser = new ProjectUserPermissionDto
            {
                ManageProjectPermission = true,
                ManageCommunityPermission = true,
                ManageFulfillmentPermission = true
            };

            return await Task.FromResult(new GetAddProjectFormResponse()
            {
                Form = new AddProjectRequest(),
                ProjectCampaignContentType = EnumHelper.EnumToList<ProjectCampaignContentType>(),
                CurrentUser = currentUser
            });
        }
    }

    public class GetAddProjectFormResponse
    {
        public AddProjectRequest Form { get; set; }
        public List<EnumItem> ProjectCampaignContentType { get; set; } = [];
        public ProjectUserPermissionDto? CurrentUser { get; set; }
    }
}
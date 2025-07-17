
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Application.Services.Projects
{
    public class ProjectCampaignService : IProjectCampaignService
    {
        public Task SetCampaign(Project project, List<ProjectCampaingContentDto> projectCampaignContents, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

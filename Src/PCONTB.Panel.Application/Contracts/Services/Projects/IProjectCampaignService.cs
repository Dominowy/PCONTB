using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Campaigns;

namespace PCONTB.Panel.Application.Contracts.Services.Projects
{
    public interface IProjectCampaignService
    {
        Task SetCampaign(Project project, ProjectCampaignDto campaign, CancellationToken token);
        Task<ProjectCampaignDto> GetCampaign(Project project, CancellationToken token);

    }
}

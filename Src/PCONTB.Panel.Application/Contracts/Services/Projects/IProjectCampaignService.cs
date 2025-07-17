using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Application.Contracts.Services.Projects
{
    public interface IProjectCampaignService
    {
        public Task SetCampaign(Project project, List<ProjectCampaingContentDto> projectCollaborators, CancellationToken token);
    }
}

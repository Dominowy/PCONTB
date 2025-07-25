using PCONTB.Panel.Domain.Projects.Campaigns;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class ProjectCampaignDto
    {
        public List<ProjectCampaignContentDto> Contents { get; set; } = new List<ProjectCampaignContentDto>();

        public static ProjectCampaignDto Map(ProjectCampaign campaing)
        {
            return new ProjectCampaignDto
            {
                Contents = [.. campaing.CampaignContents.Select(ProjectCampaignContentDto.Map)]
            };
        }
    }
}

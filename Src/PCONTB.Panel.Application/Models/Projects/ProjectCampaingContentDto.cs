using PCONTB.Panel.Domain.Projects.Campaigns;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class ProjectCampaingContentDto
    {
        public int Order { get; set; }
        public ProjectCampaignContentType Type { get; set; }
        public string? Data { get; set; }
    }
}

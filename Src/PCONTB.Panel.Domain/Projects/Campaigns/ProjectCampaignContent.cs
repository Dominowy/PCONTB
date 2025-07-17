using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Campaigns
{
    public class ProjectCampaignContent : BaseEntity
    {
        public int Order { get; set; }

        public Guid CampaignId { get; set; }
        public ProjectCampaign Campaign { get; set; }

        public ProjectCampaignContentType Type { get; set; }
        public string? Data { get; set; }

        protected ProjectCampaignContent() : base()
        {
            
        }

        public ProjectCampaignContent(int order, Guid campaignId, ProjectCampaignContentType type, string? data = null) : base()
        {
            Order = order;
            CampaignId = campaignId;
            Type = type;
            Data = data;
        }
    }
}

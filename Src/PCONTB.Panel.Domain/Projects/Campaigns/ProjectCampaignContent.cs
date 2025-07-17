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

        public ProjectCampaignContent(int order, ProjectCampaignContentType type, string? data = null) : base()
        {
            Order = order;
            Type = type;
            Data = data;
        }

        public void SetType(ProjectCampaignContentType type)
        {
            var anyChange = Type != type;
            if (!anyChange) return;

            Type = type;
        }

        public void SetData(string? data)
        {
            var anyChange = Data != data;
            if (!anyChange) return;

            Data = data;
        }
    }
}

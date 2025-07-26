using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Campaigns
{
    public class ProjectCampaign : BaseEntityEnabled
    {

        public List<ProjectCampaignContent> CampaignContents { get; set; } = [];

        protected ProjectCampaign() : base()
        {

        }

        public ProjectCampaign(Guid id) : base(id)
        {

        }

        public void AddContent(ProjectCampaignContent entity)
        {
            if (entity == null) return;

            CampaignContents.Add(entity);
        }

        public void RemoveContent(ProjectCampaignContent entity)
        {
            if (entity == null) return;

            CampaignContents.Remove(entity);
        }
    }
}

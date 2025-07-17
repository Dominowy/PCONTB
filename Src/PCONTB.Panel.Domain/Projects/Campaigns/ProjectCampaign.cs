using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Domain.Projects.Campaigns
{
    public class ProjectCampaign : BaseEntity
    {
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        public List<ProjectCampaignContent> CampaignContents { get; set; } = [];

        protected ProjectCampaign() : base()
        {
            
        }

        public ProjectCampaign(Guid projectId) : base()
        {
            ProjectId = projectId;
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

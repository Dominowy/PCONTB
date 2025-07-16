using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Campaign
{
    public class ProjectCampaign : BaseEntity
    {
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
    }
}

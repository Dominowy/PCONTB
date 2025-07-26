using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Communites
{
    public class ProjectCommunity : BaseAggregateEnabled
    {
        public Guid ProjectId { get; private set; }
        public Project Project { get; private set; }

        public virtual List<ProjectCommunityMessage> Messages { get; private set; } = [];
    }
}

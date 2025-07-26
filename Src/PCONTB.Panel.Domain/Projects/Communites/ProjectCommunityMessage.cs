using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Communites
{
    public class ProjectCommunityMessage : BaseEntityEnabled
    {
        public string Message { get; private set; }
        public string WalletAddress { get; private set; }

        public Guid ProjectCommunityId { get; private set; }
        public ProjectCommunity ProjectCommunity { get; private set; }

        public Guid? UserId { get; private set; }
        public User? User { get; private set; }

        public Guid? ParentId { get; private set; }
        public virtual ProjectCommunityMessage? Parent { get; private set; }
        public virtual List<ProjectCommunityMessage> Replies { get; private set; } = new List<ProjectCommunityMessage>();

    }
}
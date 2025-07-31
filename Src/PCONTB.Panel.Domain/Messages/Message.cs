using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Communites;

namespace PCONTB.Panel.Domain.Community
{
    public class Message : BaseAggregateEnabled
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string WalletAddress { get; private set; }

        public Guid? ProjectCommunityId { get; private set; }
        public virtual ProjectCommunity? ProjectCommunity { get; private set; }

        public Guid? UserId { get; private set; }
        public virtual User? User { get; private set; }

        public Guid? ParentId { get; private set; }
        public virtual Message? Parent { get; private set; }
        public virtual List<Message> Replies { get; private set; } = [];
    }
}

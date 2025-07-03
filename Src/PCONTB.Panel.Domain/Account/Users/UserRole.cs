using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account.Users
{
    public class UserRole : BaseEntity
    {
        public Role Role { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public UserRole(Role role, Guid userId) : base(Guid.NewGuid())
        {
            Role = role;
            UserId = userId;
        }
    }
}

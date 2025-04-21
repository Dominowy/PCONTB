using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account.Users
{
    public class UserRole : Entity
    {
        public Role Role { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public UserRole(Role role, Guid userId) : base(Guid.NewGuid())
        {
            Role = role;
            UserId = userId;
        }
    }
}

using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account.Users.Roles
{
    public class UserRole : BaseEntity
    {
        public Role Role { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public UserRole() : base()
        {

        }

        public UserRole(Guid id) : base(id)
        {

        }

        public UserRole(Role role, Guid userId) : base()
        {
            Role = role;
            UserId = userId;
        }
    }
}

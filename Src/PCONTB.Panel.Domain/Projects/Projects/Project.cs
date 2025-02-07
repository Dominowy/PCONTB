using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Projects
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

    }
}

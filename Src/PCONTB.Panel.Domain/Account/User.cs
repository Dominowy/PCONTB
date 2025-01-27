using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User() { }

    }
}

using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Account.Users.Roles;

namespace PCONTB.Panel.Application.Models.Account.Auth
{
    public class SessionUserDto : EntityDto
    {
        public string Username { get; set; }
        public List<Role> Roles { get; set; }

        public static SessionUserDto Map(User user)
        {
            return new SessionUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Roles = [.. user.Roles.Select(m => m.Role)],
            };
        }
    }
}

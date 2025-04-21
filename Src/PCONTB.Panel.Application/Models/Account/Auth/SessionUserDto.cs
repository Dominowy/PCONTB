using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Account.Users;

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
                Roles = [.. user.UserRoles.Select(m => m.Role)],
            };
        }
    }
}

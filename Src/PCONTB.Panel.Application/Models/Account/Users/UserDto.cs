using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Models.Account.Users
{
    public class UserDto : EntityDto
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public static UserDto Map(User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
            };
        }
    }
}

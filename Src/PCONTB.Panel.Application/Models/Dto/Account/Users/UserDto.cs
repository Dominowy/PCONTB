using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Models.Dto.Account.Users
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public static UserDto Map(User user)
        {
            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
            };
        }
    }
}

using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Models.Dto.Account.Users
{
    public class UserDto
    {
        public string Username { get; private set; }
        public string Email { get; private set; }

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

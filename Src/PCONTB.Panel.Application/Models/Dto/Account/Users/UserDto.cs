using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Models.Dto.Account.Users
{
    public class UserDto : EntityDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public static UserDto Map(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}

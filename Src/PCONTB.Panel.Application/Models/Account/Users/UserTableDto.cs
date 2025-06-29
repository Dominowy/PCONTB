using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Models.Account.Users
{
    public class UserTableDto : EntityDto
    {
        public string Username { get; set; }

        public string Email { get; set; }
        public virtual List<UserRoleDto> UserRoles { get; set; }

        public static UserTableDto Map(User user)
        {
            return new UserTableDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                UserRoles = [.. user.UserRoles.Select(UserRoleDto.Map)]
            };
        }
    }

    public class UserRoleDto
    {
        public string Name { get; set; }

        public static UserRoleDto Map(UserRole role)
        {
            return new UserRoleDto
            {
                Name = role.Role.ToString()
            };
        }
    }
}

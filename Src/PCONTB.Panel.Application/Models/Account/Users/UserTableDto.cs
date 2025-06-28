using PCONTB.Panel.Application.Common.Models.Dto;

namespace PCONTB.Panel.Application.Models.Account.Users
{
    public class UserTableDto : EntityDto
    {
        private List<string> list;

        public UserTableDto(string username, string email, string password, List<string> list)
        {
            Username = username;
            Email = email;
            Password = password;
            this.list = list;
        }

        public string Username { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public virtual List<UserRoleDto> UserRoles { get; set; }
    }

    public class UserRoleDto
    {
        public string Name { get; set; }
    }
}

using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Models.Account.Users
{
    public class UserTableDto : EntityDto
    {
        public string Username { get; set; }

        public string Email { get; set; }
        public virtual List<UserRoleTableDto> UserRoles { get; set; }
    }

    public class UserRoleTableDto
    {
        public string Name { get; set; }
    }
}

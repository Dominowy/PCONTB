using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class ProjectUserDto : EntityDto
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public static ProjectUserDto Map(User user)
        {
            if (user == null) return null;

            return new ProjectUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
            };
        }
    }
}
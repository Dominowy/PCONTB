using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Projects.Commands.Models
{
    public class ProjectCollaboratorUserDto : EntityDto
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public static ProjectCollaboratorUserDto Map(User user)
        {
            if (user == null) return null;

            return new ProjectCollaboratorUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
            };
        }
    }
}

using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Models.Projects.Projects
{
    public class ProjectCollaboratorDto : EntityDto
    {
        public UserDto User { get; set; }
        public static ProjectCollaboratorDto Map(ProjectCollaborator m)
        {
            return new ProjectCollaboratorDto
            {
                Id = m.Id,
                User = UserDto.Map(m.User)
            };
        }
    }
}

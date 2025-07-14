using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Models.Projects.Projects
{
    public class ProjectCollaboratorDto : EntityDto
    {
        public ProjectCollaboratorUserDto User { get; set; }
        public static ProjectCollaboratorDto Map(ProjectCollaborator m)
        {
            return new ProjectCollaboratorDto
            {
                Id = m.Id,
                User = ProjectCollaboratorUserDto.Map(m.User)
            };
        }
    }
}

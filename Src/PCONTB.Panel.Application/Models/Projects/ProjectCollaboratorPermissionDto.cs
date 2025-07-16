using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class ProjectCollaboratorPermissionDto : EntityDto
    {
        public bool ManageProjectPermission { get; set; }
        public bool ManageCommunityPermission { get; set; }
        public bool ManageFulfillmentPermission { get; set; }
        public ProjectCollaboratorUserDto User { get; set; }
        public static ProjectCollaboratorPermissionDto Map(ProjectCollaborator m)
        {
            return new ProjectCollaboratorPermissionDto
            {
                Id = m.Id,
                User = ProjectCollaboratorUserDto.Map(m.User),
                ManageProjectPermission = m.ManageProjectPermission,
                ManageCommunityPermission = m.ManageCommunityPermission,
                ManageFulfillmentPermission = m.ManageFulfillmentPermission
            };
        }
    }
}

using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class ProjectUserPermissionDto : EntityDto
    {
        public bool ManageProjectPermission { get; set; }
        public bool ManageCommunityPermission { get; set; }
        public bool ManageFulfillmentPermission { get; set; }
        public static ProjectUserPermissionDto Map(ProjectCollaborator m)
        {
            return new ProjectUserPermissionDto
            {
                Id = m.Id,
                ManageProjectPermission = m.ManageProjectPermission,
                ManageCommunityPermission = m.ManageCommunityPermission,
                ManageFulfillmentPermission = m.ManageFulfillmentPermission
            };
        }
    }
}

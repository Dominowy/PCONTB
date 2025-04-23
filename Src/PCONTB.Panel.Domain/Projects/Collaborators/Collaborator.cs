using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Collaborators
{
    public class Collaborator : Entity
    {
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public bool ManageProjectPermission { get; set; }
        public bool ManageCommunityPermission { get; set; }
        public bool ManageFulfillmentPermission { get; set; }

        public Collaborator(Guid projectId, Guid userId) : base(Guid.NewGuid())
        {
            ProjectId = projectId;
            UserId = userId;
        }

        public void SetManageProjectPermission(bool permission)
        {
            ManageProjectPermission = permission;
        }

        public void SetCommunityPermission(bool permission)
        {
            ManageCommunityPermission = permission;
        }

        public void SetManageFulfillmentPermission(bool permission)
        {
            ManageFulfillmentPermission = permission;
        }
    }
}
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Collaborators
{
    public class Collaborator : Entity
    {
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public bool ManageProjectPermission { get; private set; }
        public bool ManageCommunityPermission { get; private set; }
        public bool ManageFulfillmentPermission { get; private set; }

        public Collaborator(Guid projectId, Guid userId) : base(Guid.NewGuid())
        {
            ProjectId = projectId;
            UserId = userId;
        }

        public void SetManageProjectPermission(bool permission)
        {
            var anyChange = ManageProjectPermission != permission;
            if (!anyChange) return;

            ManageProjectPermission = permission;
        }

        public void SetManageCommunityPermission(bool permission)
        {
            var anyChange = ManageCommunityPermission != permission;
            if (!anyChange) return;

            ManageCommunityPermission = permission;
        }

        public void SetManageFulfillmentPermission(bool permission)
        {
            var anyChange = ManageFulfillmentPermission != permission;
            if (!anyChange) return;

            ManageFulfillmentPermission = permission;
        }
    }
}
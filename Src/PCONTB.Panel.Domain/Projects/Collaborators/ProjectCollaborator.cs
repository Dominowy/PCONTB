using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Collaborators
{
    public class ProjectCollaborator : BaseEntity
    {
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public bool ManageProjectPermission { get; private set; }
        public bool ManageCommunityPermission { get; private set; }
        public bool ManageFulfillmentPermission { get; private set; }

        public ProjectCollaborator() : base()
        {

        }

        public ProjectCollaborator(Guid userId, Guid projectId) : base()
        {
            ProjectId = projectId;
            UserId = userId;
        }

        public ProjectCollaborator(Guid userId, Guid projectId, bool manageProjectPermission, bool manageCommunityPermission, bool manageFulfillmentPermission) : base()
        {
            ProjectId = projectId;
            UserId = userId;
            ManageProjectPermission = manageProjectPermission;
            ManageCommunityPermission = manageCommunityPermission;
            ManageFulfillmentPermission = manageFulfillmentPermission;
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
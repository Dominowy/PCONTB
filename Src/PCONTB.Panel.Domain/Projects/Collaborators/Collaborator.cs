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
    }
}
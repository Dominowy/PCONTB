using PCONTB.Panel.Application.Common.Models;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class AddProjectCollaboratorDto : EntityDto
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }
        public bool ManageProjectPermission { get; set; }
        public bool ManageCommunityPermission { get; set; }
        public bool ManageFulfillmentPermission { get; set; }
    }
}

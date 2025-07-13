using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Functions.Projects.Commands.Models
{
    public class ProjectCollaboratorDto : EntityDto
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }

        public string Email { get; set; }
        public bool ManageProjectPermission { get; set; }
        public bool ManageCommunityPermission { get; set; }
        public bool ManageFulfillmentPermission { get; set; }

        public static ProjectCollaboratorDto Map(ProjectCollaborator m)
        {
            return new ProjectCollaboratorDto
            {
                Id = m.Id,
                UserId = m.User.Id,
                ProjectId = m.ProjectId,
                Email = m.User.Email,
                ManageProjectPermission = m.ManageProjectPermission,
                ManageCommunityPermission = m.ManageCommunityPermission,
                ManageFulfillmentPermission = m.ManageFulfillmentPermission
            };
        }

        public static ProjectCollaborator Map(ProjectCollaboratorDto m, Guid projectId)
        {
            var entity = new ProjectCollaborator(m.Id, m.UserId, projectId);

            entity.SetManageProjectPermission(m.ManageProjectPermission);
            entity.SetManageCommunityPermission(m.ManageCommunityPermission);
            entity.SetManageFulfillmentPermission(m.ManageFulfillmentPermission);

            return entity;
        }
    }
}

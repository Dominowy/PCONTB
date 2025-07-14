using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Services.Projects
{
    public class ProjectCollaboratorService : IProjectCollaboratorService
    {
        public async Task AddCollaborators(Project project, List<AddProjectCollaboratorDto> projectCollaborators, CancellationToken token)
        {
            foreach (var item in projectCollaborators)
            {
                var collaborator = new ProjectCollaborator(item.UserId, project.Id, item.ManageProjectPermission, item.ManageCommunityPermission, item.ManageFulfillmentPermission);

                project.AddCollaborator(collaborator);
            }
        }

        public async Task UpdateCollaborators(Project project, List<UpdateProjectCollaboratorDto> projectCollaborators, CancellationToken token)
        {
            var collaboratorIds = projectCollaborators.Select(x => x.Id).ToHashSet();
            var collaboratorsToRemove = project.Collaborators.Where(c => !collaboratorIds.Contains(c.Id)).ToList();

            foreach (var removed in collaboratorsToRemove)
            {
                project.RemoveCollaborator(removed);
            }

            foreach (var item in projectCollaborators)
            {
                var existing = project.Collaborators.FirstOrDefault(c => c.Id == item.Id);

                if (existing == null)
                {
                    var newCollaborator = new ProjectCollaborator(item.UserId, project.Id, item.ManageProjectPermission, item.ManageCommunityPermission, item.ManageFulfillmentPermission);
                    project.AddCollaborator(newCollaborator);
                }
                else
                {
                    existing.SetManageProjectPermission(item.ManageProjectPermission);
                    existing.SetManageCommunityPermission(item.ManageCommunityPermission);
                    existing.SetManageFulfillmentPermission(item.ManageFulfillmentPermission);
                }
            }
        }
    }
}

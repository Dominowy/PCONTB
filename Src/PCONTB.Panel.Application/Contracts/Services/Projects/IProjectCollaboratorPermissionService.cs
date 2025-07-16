using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Contracts.Services.Projects
{
    public interface IProjectCollaboratorPermissionService
    {
        Task<ProjectCollaborator?> GetCurrentCollaborator(Project aggregate, CancellationToken cancellationToken);
        Task Verify(Project project, ProjectPermission permission ,CancellationToken cancellationToken);
    }
}

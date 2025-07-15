using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Application.Contracts.Services.Projects
{
    public interface IProjectCollaboratorPermissionService
    {
        Task Verify(Project project, ProjectPermission permission ,CancellationToken cancellationToken);
    }
}

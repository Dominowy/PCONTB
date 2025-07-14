using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Application.Contracts.Services.Projects
{
    public interface IProjectCollaboratorService
    {
        public Task AddCollaborators(Project project, List<AddProjectCollaboratorDto> projectCollaborators, CancellationToken token);

        public Task UpdateCollaborators(Project project, List<UpdateProjectCollaboratorDto> projectCollaborators, CancellationToken token);

    }
}

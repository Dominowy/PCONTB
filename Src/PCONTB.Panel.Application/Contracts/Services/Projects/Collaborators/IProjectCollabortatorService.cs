using PCONTB.Panel.Application.Models.Projects.Collaborators;

namespace PCONTB.Panel.Application.Contracts.Services.Projects.Collaborators
{
    public interface IProjectCollabortatorService
    {
        public Task AddCollaboratorAsync(AddUpdateProjectCollaboratorDto addProjectCollaboratorDto, CancellationToken token);
    }
}

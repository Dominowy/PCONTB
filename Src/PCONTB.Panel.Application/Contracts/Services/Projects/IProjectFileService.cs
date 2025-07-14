using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Application.Contracts.Services.Projects
{
    public interface IProjectFileService
    {
        Task UploadImage(Project project, FormFile file, CancellationToken token);
    }
}

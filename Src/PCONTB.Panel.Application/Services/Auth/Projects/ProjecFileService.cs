using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Application.Services.Auth.Projects
{
    public class ProjecFileService : IProjectFileService
    {
        public async Task UploadImage(Project project, FormFile file, CancellationToken token)
        {
            if (file != null)
            {
                if (!string.IsNullOrEmpty(file.Path))
                {
                    var data = await File.ReadAllBytesAsync(file.Path, token);

                    if (project.Image == null)
                    {
                        var image = new ProjectImage(file.FileName, file.ContentType, data);

                        project.SetImage(image);
                    }
                    else
                    {
                        project.Image.SetFileName(file.FileName);
                        project.Image.SetImageData(data);
                        project.Image.SetContentType(file.ContentType);
                    }
                }
            }
        }
    }
}

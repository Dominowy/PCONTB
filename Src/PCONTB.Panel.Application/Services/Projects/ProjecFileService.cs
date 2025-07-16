using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Application.Services.Projects
{
    public class ProjecFileService : IProjectFileService
    {
        public async Task UploadImage(Project aggregate, FormFile? file, CancellationToken token)
        {
            if (file != null)
            {
                if (!string.IsNullOrEmpty(file.Path))
                {
                    var data = await File.ReadAllBytesAsync(file.Path, token);

                    if (aggregate.Image == null)
                    {
                        var image = new ProjectImage(file.FileName, file.ContentType, data);

                        aggregate.SetImage(image);
                    }
                    else
                    {
                        aggregate.Image.SetFileName(file.FileName);
                        aggregate.Image.SetImageData(data);
                        aggregate.Image.SetContentType(file.ContentType);
                    }
                }
            }
        }

        public async Task UploadVideo(Project aggregate, FormFile? file, CancellationToken token)
        {
            if (file != null)
            {
                if (!string.IsNullOrEmpty(file.Path))
                {
                    var data = await File.ReadAllBytesAsync(file.Path, token);

                    if (aggregate.Video == null)
                    {
                        var video = new ProjectVideo(file.FileName, file.ContentType, data);

                        aggregate.SetVideo(video);
                    }
                    else
                    {
                        aggregate.Video.SetFileName(file.FileName);
                        aggregate.Video.SetImageData(data);
                        aggregate.Video.SetContentType(file.ContentType);
                    }
                }
            }
        }
    }
}

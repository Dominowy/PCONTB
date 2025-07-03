using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Projects.Files
{
    public class ProjectImage : ProjectFile
    {
        public ProjectImage(Guid id) : base(id)
        {
        }

        public ProjectImage(string fileName, string contentType, byte[] data) : base(Guid.NewGuid())
        {
            SetFileName(fileName);
            SetContentType(contentType);
            SetImageData(data);
        }
    }
}

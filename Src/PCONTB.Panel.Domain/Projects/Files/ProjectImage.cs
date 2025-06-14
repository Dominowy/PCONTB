using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Files
{
    public class ProjectImage : ProjectFile
    {
        public ProjectImage(Guid id) : base(id)
        {
        }

        public ProjectImage(Guid id, string fileName, string contentType, byte[] data) : base(id)
        {
            SetFileName(fileName);
            SetContentType(contentType);
            SetImageData(data);
        }
    }
}

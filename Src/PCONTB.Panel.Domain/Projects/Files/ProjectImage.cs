namespace PCONTB.Panel.Domain.Projects.Files
{
    public class ProjectImage : ProjectFile
    {
        public Project Project { get; private set; }

        public ProjectImage() : base()
        {

        }

        public ProjectImage(Guid id) : base(id)
        {

        }

        public ProjectImage(string fileName, string contentType, byte[] data) : base()
        {
            SetFileName(fileName);
            SetContentType(contentType);
            SetImageData(data);
        }
    }
}

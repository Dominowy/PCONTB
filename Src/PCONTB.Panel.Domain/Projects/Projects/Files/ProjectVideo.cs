namespace PCONTB.Panel.Domain.Projects.Projects.Files
{
    public class ProjectVideo : ProjectFile
    {
        protected ProjectVideo() : base()
        {
            
        }

        public ProjectVideo(Guid id) : base(id)
        {

        }

        public ProjectVideo(string fileName, string contentType, byte[] data) : base()
        {
            SetFileName(fileName);
            SetContentType(contentType);
            SetImageData(data);
        }
    }
}

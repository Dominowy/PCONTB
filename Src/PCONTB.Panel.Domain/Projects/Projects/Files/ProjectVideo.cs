namespace PCONTB.Panel.Domain.Projects.Projects.Files
{
    public class ProjectVideo : ProjectFile
    {
        protected ProjectVideo() : base()
        {
            
        }

        public ProjectVideo(string fileName, string contentType, byte[] data) : base(Guid.NewGuid())
        {
            SetFileName(fileName);
            SetContentType(contentType);
            SetImageData(data);
        }
    }
}

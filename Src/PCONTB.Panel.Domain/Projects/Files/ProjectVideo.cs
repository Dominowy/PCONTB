namespace PCONTB.Panel.Domain.Projects.Files
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

using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Files
{
    public abstract class ProjectFile : BaseEntity
    {
        public byte[] Data { get; private set; }
        public string FileName { get; private set; }
        public string ContentType { get; private set; }

        protected ProjectFile() : base()
        {
        }

        protected ProjectFile(Guid id) : base(id)
        {
        }

        public void SetFileName(string imageName)
        {
            var anyChange = FileName != imageName;
            if (!anyChange) return;

            FileName = imageName;
        }

        public void SetImageData (byte[] imageData)
        {
            var anyChange = Data != imageData;
            if (!anyChange) return;

            Data = imageData;
        }

        public void SetContentType(string contentType)
        {
            var anyChange = ContentType != contentType;
            if (!anyChange) return;

            ContentType = contentType;
        }
    }
}

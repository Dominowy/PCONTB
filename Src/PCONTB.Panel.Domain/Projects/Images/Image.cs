using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Images
{
    public class Image : Entity
    {
        public byte[] ImageData { get; private set; }
        public string ImageName { get; private set; } 

        public int DisplayOrder { get; private set; }

        public Guid ProjectId { get; private set; }
        public Project Project { get; private set; }


        public Image(Guid projectId) : base(Guid.NewGuid()) 
        { 
            ProjectId = projectId;
        }

        public void SetDispalyOrder(int displayOrder)
        {
            var anyChange = DisplayOrder != displayOrder;
            if (!anyChange) return;

            DisplayOrder = displayOrder;
        }

        public void SetImageName(string imageName)
        {
            var anyChange = ImageName != imageName;
            if (!anyChange) return;

            ImageName = imageName;
        }

        public void SetImageData (byte[] imageData)
        {
            var anyChange = ImageData != imageData;
            if (!anyChange) return;

            ImageData = imageData;
        }
    }
}

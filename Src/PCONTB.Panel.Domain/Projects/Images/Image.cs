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


        public Image(byte[] imageData, string imageName, int displayOrder, Guid projectId) : base(Guid.NewGuid()) 
        { 
            ImageData = imageData;
            ImageName = imageName;
            DisplayOrder = displayOrder;
            ProjectId = projectId;
        }
    }
}

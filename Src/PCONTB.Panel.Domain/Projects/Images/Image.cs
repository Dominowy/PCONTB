using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.ProjectImages
{
    public class Image : Entity
    {
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; } 

        public int DisplayOrder { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

    }
}

using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Subcategory : Entity
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}

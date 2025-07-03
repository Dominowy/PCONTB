using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Subcategory : BaseAggregateNameEnabled
    {
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Project> Projects { get; set; }

        public Subcategory(string name, Guid categoryId) : base(Guid.NewGuid())
        {
            SetName(name);
            CategoryId = categoryId;
        }
    }
}

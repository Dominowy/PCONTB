using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class CategorySubcategory : BaseEntityNameEnabled
    {
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Project> Projects { get; set; }

        public CategorySubcategory(string name, Guid categoryId) : base(Guid.NewGuid())
        {
            SetEnabled(true);
            SetName(name);
            CategoryId = categoryId;
        }
    }
}

using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class CategorySubcategory : BaseEntityNameEnabled
    {
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Project> Projects { get; set; } = new List<Project>();

        public CategorySubcategory() : base()
        {
            SetEnabled(true);
        }

        public CategorySubcategory(Guid id) : base(id)
        {
            SetEnabled(true);
        }

        public CategorySubcategory(string name, Guid categoryId) : base()
        {
            SetEnabled(true);
            SetName(name);
            CategoryId = categoryId;
        }
    }
}

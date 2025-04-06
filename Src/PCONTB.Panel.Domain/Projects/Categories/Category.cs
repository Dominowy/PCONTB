using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual List<Subcategory> Subcategories { get; set; }

        public virtual List<Project> Projects { get; set; }
    }
}

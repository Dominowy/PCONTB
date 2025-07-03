using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Category : BaseAggregateNameEnabled
    {
        public virtual List<Subcategory> Subcategories { get; set; }
        public virtual List<Project> Projects { get; set; }
        

        public Category(string name) : base(Guid.NewGuid())
        {
            SetName(name);
        }

        public void SetSubcategories(List<Subcategory> subCategories)
        {
            Subcategories = subCategories;
        }
    }
}

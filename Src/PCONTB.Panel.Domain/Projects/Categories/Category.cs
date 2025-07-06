using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Category : BaseAggregateNameEnabled
    {
        public virtual List<CategorySubcategory> Subcategories { get; private set; } = new List<CategorySubcategory>();
        public virtual List<Project> Projects { get; private set; } = new List<Project>();
        

        public Category(string name) : base(Guid.NewGuid())
        {
            SetName(name);
        }

        public void SetSubcategories(List<CategorySubcategory> subCategories)
        {
            Subcategories = subCategories;
        }
    }
}

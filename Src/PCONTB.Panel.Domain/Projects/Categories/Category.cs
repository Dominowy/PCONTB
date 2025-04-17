using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual List<Subcategory> Subcategories { get; set; }

        public virtual List<Project> Projects { get; set; }

        public Category(string name) : base(Guid.NewGuid())
        {
            Name = name;
        }

        public void ChangeName(string name)
        {
            var anyChange = Name != name;
            if (!anyChange) return;

            Name = name;
        }

        public void SetSubcategories(List<Subcategory> subCategories)
        {
            Subcategories = subCategories;
        }
    }
}

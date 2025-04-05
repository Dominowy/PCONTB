using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual List<Subcategory> Subcategories { get; set; }
    }
}

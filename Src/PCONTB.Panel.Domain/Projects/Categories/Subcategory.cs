using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Projects.Categories
{
    public class Subcategory : EntityName
    {
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Project> Projects { get; set; }

        public Subcategory(string name, Guid categoryId) : base(Guid.NewGuid())
        {
            Name = name;
            CategoryId = categoryId;
        }

        public void SetName(string name)
        {
            var anyChange = Name != name;
            if (!anyChange) return;

            Name = name;
        }
    }
}

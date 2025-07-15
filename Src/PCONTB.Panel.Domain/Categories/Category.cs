using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Domain.Categories
{
    public class Category : BaseAggregateNameEnabled
    {
        public virtual List<Project> Projects { get; private set; } = new List<Project>();
        
        public Category(string name) : base(Guid.NewGuid())
        {
            SetName(name);
        }
    }
}

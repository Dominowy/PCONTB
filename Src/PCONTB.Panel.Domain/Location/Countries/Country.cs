using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Domain.Location.Countries
{
    public class Country : BaseAggregateNameEnabled
    {
        public virtual List<Project> Projects { get; set; }

        public Country(string name, bool enabled) : base(Guid.NewGuid())
        {
            SetName(name);
            SetEnabled(enabled);
        }
    }
}

using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Location.Countries
{
    public class Country : EntityName
    {
        public virtual List<Project> Projects { get; set; }

        public Country(string name) : base(Guid.NewGuid())
        {
            Name = name;
        }

        public void SetName(string name)
        {
            var anyChange = Name != name;
            if (!anyChange) return;

            Name = name;
        }
    }
}

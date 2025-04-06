using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Location.Countries
{
    public class Country : Entity
    {
        public string Name { get; set; }

        public virtual List<Project> Projects { get; set; }
    }
}

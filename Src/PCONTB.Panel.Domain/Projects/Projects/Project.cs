using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.ProjectImages;
using System.Xml.Linq;

namespace PCONTB.Panel.Domain.Projects.Projects
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Guid? SubcategoryId { get; set; }
        public virtual Subcategory? Subcategory { get; set; }

        public virtual List<Collaborator> Collaborators { get; set; }

        public virtual List<Image> Images { get; set; }

        protected Project() : base()
        {
        }

        public Project(Guid id, string name, Guid userId) : base(id)
        {
            Name = name;
            UserId = userId;
        }

        public Project(Guid id, string name, Guid userId, Guid countryId, Guid categoryId, Guid? subcategoryId) : base(id)
        {
            Name = name;
            UserId = userId;
            CountryId = countryId;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
        }

        public void SetName(string name)
        {
            var anyChange = Name != name;
            if (!anyChange) return;

            Name = name;
        }

        public void SetUser(Guid userId)
        {
            var anyChange = UserId != userId;
            if (!anyChange) return;

            UserId = userId;
        }

        public void SetCountry(Guid countryId)
        {
            var anyChange = CountryId != countryId;
            if (!anyChange) return;

            CountryId = countryId;
        }

        public void SetCategory(Guid categoryId)
        {
            var anyChange = CategoryId != categoryId;
            if (!anyChange) return;

            CategoryId = categoryId;
        }

        public void SetSubcategory(Guid? subcategoryId)
        {
            var anyChange = SubcategoryId != subcategoryId;
            if (!anyChange) return;

            SubcategoryId = subcategoryId;
        }
    }
}
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Domain.Projects.Projects
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }

        public Guid PrimaryCategoryId { get; set; }
        public virtual Category PrimaryCategory { get; set; }

        public Guid? PrimarySubcategoryId { get; set; }
        public virtual Subcategory? PrimarySubcategory { get; set; }

        public Guid AdditionalCategoryId { get; set; }
        public virtual Category AdditionalCategory { get; set; }

        public Guid? AdditionalSubcategoryId { get; set; }
        public virtual Subcategory? AdditionalSubcategory { get; set; }


        protected Project() : base() { }

        public Project(Guid id, string name, Guid userId) : base(id)
        {
            Name = name;
            UserId = userId;
        }

        public void UpdateProject(string name, Guid userId)
        {
            SetName(name);
            SetUser(userId);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetUser(Guid userId) 
        { 
            UserId = userId;
        }
    }
}

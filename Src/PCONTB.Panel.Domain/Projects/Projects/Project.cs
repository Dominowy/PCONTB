using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.ProjectImages;

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

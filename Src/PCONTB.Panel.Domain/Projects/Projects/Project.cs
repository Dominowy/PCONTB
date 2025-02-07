using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Projects.Projects
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }


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

using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Account.Users
{
    public class User : Entity
    {
        public string Username { get; private set; }

        public string Email { get; private set; }
        public string Password { get; private set; }

        public virtual List<Project> Projects { get; private set; }
        public virtual List<Collaborator> Collaborators { get; private set; }
        public virtual List<Session> Sessions { get; private set; }
        public virtual Role Role { get; private set; }

        protected User() : base()
        {
        }

        public User(Guid id, string username, string email, string password) : base(id)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = Role.User;
        }

        public User(Guid id, string username, string email, string password, Role role) : base(id)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }

        public void SetUsername(string username)
        {
            var anyChange = Username != username;
            if (!anyChange) return;

            Username = username;
        }

        public void SetEmail(string email)
        {
            var anyChange = Email != email;
            if (!anyChange) return;

            Email = email;
        }

        public void SetPassword(string password)
        {
            var anyChange = Password != password;
            if (!anyChange) return;

            Password = password;
        }

        public void SetRole(Role role)
        {
            var anyChange = Role != role;
            if (!anyChange) return;

            Role = role;
        }
    }
}
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

        protected User() : base() { }

        public User(Guid id, string username, string email, string password) : base(id)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public void ChangeUsername(string username)
        {
            var anyChange = Username != username;
            if (!anyChange) return;

            Username = username;
        }

        public void ChangeEmail(string email)
        {
            var anyChange = Email != email;
            if (!anyChange) return;

            Email = email;
        }

        public void ChangePassword(string password)
        {
            var anyChange = Password != password;
            if (!anyChange) return;

            Password = password;
        }
    }
}

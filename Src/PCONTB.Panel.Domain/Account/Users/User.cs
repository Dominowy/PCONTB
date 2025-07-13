using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Domain.Account.Users
{
    public class User : BaseAggregateEnabled
    {
        public string Username { get; private set; }

        public string Email { get; private set; }
        public string Password { get; private set; }

        public virtual List<Project> Projects { get; private set; } = new List<Project>();
        public virtual List<ProjectCollaborator> Collaborators { get; private set; } = new List<ProjectCollaborator>();
        public virtual List<Session> Sessions { get; private set; } = new List<Session>();
        public virtual List<UserRole> UserRoles { get; private set; } = new List<UserRole>();

        protected User() : base()
        {
        }

        public User(Guid id, string username, string email, string password) : base(id)
        {
            Username = username;
            Email = email;
            Password = password;
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
    }
}
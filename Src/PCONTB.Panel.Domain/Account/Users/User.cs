using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Domain.Account.Users
{
    public class User : Entity
    {
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                {
                    return $"{FirstName} {LastName}";
                }
                return $"{Username}";
            }
        }

        public string Email { get; private set; }
        public string Password { get; private set; }

        public virtual List<Project> Projects { get; private set; }

        protected User() : base() { }

        public User(Guid id, string firstName, string lastName, string username, string email, string password) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Password = password;
        }

        public void UpdateUser(string firstName, string lastName, string username, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
        }

        public void ChangeUsername(string username)
        {
            var anyChange = Username != username;
            Username = username;
        }

        public void ChangeName(string firstName, string lastName)
        {
            var anyChange = FirstName != firstName && LastName != lastName;
            if (!anyChange) return;

            FirstName = firstName;
            LastName = lastName;
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

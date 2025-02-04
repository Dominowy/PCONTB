using FluentAssertions;
using PCONTB.Panel.Domain.Account.User;

namespace PCONTB.Panel.Domain.Tests.Account
{

    public class UserTest
    {
        private readonly User _user;
        private readonly string _newFirstName = "Will";
        private readonly string _newLastName = "Kane";
        private readonly string _newUsername = "WillKane";
        private readonly string _newEmail = "will@example.com";
        private readonly string _newPassword = "password1";

        public UserTest()
        {
            _user = new User(Guid.NewGuid(), "John", "Smith", "JohnSmith", "john@example.com", "password");
        }

        [Fact]
        public void ShouldBeTypeUser()
        {
            _user.Should().BeOfType<User>();
        }

        [Fact]
        public void ShouldChangeName()
        {
            _user.ChangeName(_newFirstName, _newLastName);
            _user.DisplayName.Should().Be(_newFirstName + " " + _newLastName);
        }

        [Fact]
        public void ShouldChangeUsername()
        {
            _user.ChangeUsername(_newUsername);
            _user.Username.Should().Be(_newUsername);
        }

        [Fact]
        public void ShouldChangeEmail()
        {
            _user.ChangeEmail(_newEmail);
            _user.Email.Should().Be(_newEmail);
        }

        [Fact]
        public void ShouldChangePassword()
        {
            _user.ChangePassword(_newPassword);
            _user.Password.Should().Be(_newPassword);
        }
    }
}

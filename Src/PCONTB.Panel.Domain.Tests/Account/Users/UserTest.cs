using FluentAssertions;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Domain.Tests.Account.Users
{

    public class UserTest
    {
        private readonly User _user;
        private readonly string _newUsername = "WillKane";
        private readonly string _newEmail = "will@example.com";
        private readonly string _newPassword = "password1";

        public UserTest()
        {
            _user = new User(Guid.NewGuid(), "JohnSmith", "john@example.com", "password");
        }

        [Fact]
        public void ShouldBeTypeUser()
        {
            _user.Should().BeOfType<User>();
        }

        [Fact]
        public void ShouldChangeUsername()
        {
            _user.SetUsername(_newUsername);
            _user.Username.Should().Be(_newUsername);
        }

        [Fact]
        public void ShouldChangeEmail()
        {
            _user.SetEmail(_newEmail);
            _user.Email.Should().Be(_newEmail);
        }

        [Fact]
        public void ShouldChangePassword()
        {
            _user.SetPassword(_newPassword);
            _user.Password.Should().Be(_newPassword);
        }
    }
}

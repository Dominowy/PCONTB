using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Projects.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCONTB.Panel.Application.Models.Dto.Account.Users
{
    public class UserDto
    {
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        public static UserDto Map(User user)
        {
            return new UserDto
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.DisplayName,
                Email = user.Email,
            };
        }
    }
}

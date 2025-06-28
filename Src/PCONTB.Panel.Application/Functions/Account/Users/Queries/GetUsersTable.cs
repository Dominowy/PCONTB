using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Application.Table;
using PCONTB.Panel.Domain.Account.Users;
using System.Linq.Expressions;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class UserPagedQueryHandler : PagedQueryHandler<User, UserTableDto>
    {
        public UserPagedQueryHandler(DbContext context) : base(context) { }

        protected override string[] GetGlobalSearchProperties()
        {
            return new[] { "Username", "Email", "UserRoles.Role" };
        }

        protected override UserTableDto MapEntityToDto(User user)
        {
            return new UserTableDto(
                user.Username,
                user.Email,
                user.Password,
                user.UserRoles.Select(r => r.Role.ToString()).ToList()
            );
        }
    }

}

using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Application.Table;
using PCONTB.Panel.Domain.Account.Users;
using System.Data;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class UserPagedQueryHandler : PagedQueryHandler<User, UserTableDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public UserPagedQueryHandler(IApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public override async Task<PagedResultDto<UserTableDto>> Handle(PagedQueryRequest<UserTableDto> request, CancellationToken cancellationToken)
        {
            IQueryable<User> query = GetQuery();

            SetQuery(query);

            return await base.Handle(request, cancellationToken);
        }

        protected override IQueryable<User> GetQuery()
        {
            return _dbContext.Set<User>()
                .Include(u => u.UserRoles);
        }

        protected override string[] GetGlobalSearchProperties()
        {
            return new[] { "Username", "Email", "UserRoles.Role" };
        }

        protected override UserTableDto MapEntityToDto(User user)
        {
            return new UserTableDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                UserRoles = [.. user.UserRoles.Select(m =>  new UserRoleTableDto
                {
                    Name = m.Role.ToString()
                })]
            };
        }
    }

}

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
            IQueryable<User> query = _dbContext.Set<User>()
                .Include(u => u.UserRoles);

            if (request.Filters?.TryGetValue("userRoles", out var roleValue) == true)
            {
                query = query.Where(u =>
                    u.UserRoles.Any(ur => ur.Role.ToString().Contains(roleValue)));
                request.Filters.Remove("userRoles");
            }

            var sort = request.Sorts.FirstOrDefault(s => s.Field == "userRoles");
            if (sort != null)
            {
                request.Sorts.Remove(sort);
                query = sort.Descending
                    ? query.OrderByDescending(u => u.UserRoles.Select(ur => ur.Role.ToString()).FirstOrDefault())
                    : query.OrderBy(u => u.UserRoles.Select(ur => ur.Role.ToString()).FirstOrDefault());
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var normalized = request.Search.ToLowerInvariant();

                query = query.Where(u =>
                    u.Username.ToLower().Contains(normalized) ||
                    u.Email.ToLower().Contains(normalized) ||
                    u.UserRoles.Any(ur => ur.Role.ToString().ToLower().Contains(normalized))
                );

                request.Search = null;
            }

            SetQuery(query);

            return await base.Handle(request, cancellationToken);
        }

        protected virtual IQueryable<User> GetQuery()
        {
            return _dbContext.Set<User>()
                .Include(u => u.UserRoles);
        }

        protected override string[] GetGlobalSearchProperties()
        {
            return new[] { "Username", "Email" };
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

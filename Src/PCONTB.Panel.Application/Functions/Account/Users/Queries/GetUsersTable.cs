using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Account.Users;
using System.Linq.Expressions;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUsersTableRequest : IRequest<PaginatedResult<UserTableDto>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = null;
        public Dictionary<string, string> Filters { get; set; } = new();
        public List<SortOption> Sorts { get; set; } = new();
    }

    public class GetUsersTableHandler : IRequestHandler<GetUsersTableRequest, PaginatedResult<UserTableDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUsersTableHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<UserTableDto>> Handle(GetUsersTableRequest request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<User>()
                .Include(u => u.UserRoles)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                query = query.Where(u =>
                    u.Username.ToLower().Contains(searchLower) ||
                    u.Email.ToLower().Contains(searchLower) ||
                    u.UserRoles.Any(r => r.Role.ToString().ToLower().Contains(searchLower))
                );
            }

            foreach (var filter in request.Filters)
            {
                var value = filter.Value.ToLower();

                if (filter.Key == "username")
                    query = query.Where(u => u.Username.ToLower().Contains(value));

                if (filter.Key == "email")
                    query = query.Where(u => u.Email.ToLower().Contains(value));

                if (filter.Key == "userRoles")
                    query = query.Where(u => u.UserRoles.Any(m => m.Role.ToString().ToLower().Contains(value)));

            }

            if (request.Sorts != null && request.Sorts.Any())
            {
                IOrderedQueryable<User> orderedQuery = null;
                foreach (var sort in request.Sorts)
                {
                    if (orderedQuery == null)
                    {
                        orderedQuery = sort.Descending
                            ? query.OrderByDescending(GetPropertyExpression(sort.Field))
                            : query.OrderBy(GetPropertyExpression(sort.Field));
                    }
                    else
                    {
                        orderedQuery = sort.Descending
                            ? orderedQuery.ThenByDescending(GetPropertyExpression(sort.Field))
                            : orderedQuery.ThenBy(GetPropertyExpression(sort.Field));
                    }
                }
                query = orderedQuery;
            }
            else
            {
                query = query.OrderBy(u => u.Username);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var users = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserTableDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    UserRoles = u.UserRoles.Select(r => new UserRoleDto { Name = r.Role.ToString() }).ToList()
                })
                .ToListAsync(cancellationToken);

            return new PaginatedResult<UserTableDto>
            {
                Items = users,
                TotalCount = totalCount
            };
        }

        private static Expression<Func<User, object>> GetPropertyExpression(string propertyName)
        {
            var param = Expression.Parameter(typeof(User), "x");
            var body = Expression.Convert(Expression.PropertyOrField(param, propertyName), typeof(object));
            return Expression.Lambda<Func<User, object>>(body, param);
        }
    }


    public class SortOption
    {
        public string Field { get; set; }
        public bool Descending { get; set; }
    }

    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}

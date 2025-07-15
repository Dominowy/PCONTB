using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<User> GetQuery()
        {
            return dbSet.Include(m => m.UserRoles).AsNoTracking();
        }

        public override async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(m => m.UserRoles)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public override async Task<User?> GetBy(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .Include(m => m.UserRoles)
                .Include(m => m.Projects).ThenInclude(m => m.Category)
                .Include(m => m.Projects).ThenInclude(m => m.Country)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByTracking(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .Include(m => m.UserRoles)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

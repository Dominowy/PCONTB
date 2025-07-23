using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) 
        : Repository<User>(context), IUserRepository
    {
        public override IQueryable<User> GetQuery()
        {
            return dbSet.Include(m => m.Roles).AsNoTracking();
        }

        public override async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(m => m.Roles)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public override async Task<User?> GetBy(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .Include(m => m.Roles)
                .Include(m => m.Projects).ThenInclude(m => m.Category)
                .Include(m => m.Projects).ThenInclude(m => m.Country)
                .Include(m => m.Collaborators).ThenInclude(m => m.Project).ThenInclude(m => m.Category)
                .Include(m => m.Collaborators).ThenInclude(m => m.Project).ThenInclude(m => m.Country)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByTracking(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .Include(m => m.Roles)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

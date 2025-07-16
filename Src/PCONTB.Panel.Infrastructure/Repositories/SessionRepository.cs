using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class SessionRepository(ApplicationDbContext context) 
        : Repository<Session>(context), ISessionRepository
    {
        public virtual async Task<Session?> GetBy(Expression<Func<Session, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(m => m.User)
                .ThenInclude(m => m.UserRoles)
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

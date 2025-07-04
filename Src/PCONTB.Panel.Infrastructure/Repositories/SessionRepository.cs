using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Session> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return await dbSet.Include(m => m.User).ThenInclude(m => m.UserRoles)
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
    }
}

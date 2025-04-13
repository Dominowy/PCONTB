using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Sessions;

namespace PCONTB.Panel.Application.Services.Auth
{
    public class SessionService : ISessionService
    {
        private readonly IApplicationDbContext _dbContext;

        public SessionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session?> GetByIdAsync(Guid? sessionId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Session>()
                .Include(m => m.User)
                .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);
        }

        public async Task<Guid> CreateSession(Guid userId, CancellationToken cancellationToken)
        {
            var session = new Session(Guid.NewGuid(), userId);

            await _dbContext.Set<Session>().AddAsync(session, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}

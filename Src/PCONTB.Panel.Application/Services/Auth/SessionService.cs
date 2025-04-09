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
        public async Task<Session?> GetByIdAsync(Guid sessionId)
        {
            return await _dbContext.Set<Session>()
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }
    }
}

using PCONTB.Panel.Domain.Account.Sessions;

namespace PCONTB.Panel.Application.Contracts.Application.Services.Auth
{
    public interface ISessionService
    {
        Task<Session?> GetByIdAsync(Guid? sessionId, CancellationToken cancellationToken);
        Task<Guid> CreateSession(Guid sessionId, CancellationToken cancellationToken);
        Task<bool> CheckSessionActiveState(Session session, CancellationToken cancellationToken);
    }
}

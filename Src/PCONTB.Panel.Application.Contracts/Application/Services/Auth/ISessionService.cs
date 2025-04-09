using PCONTB.Panel.Domain.Account.Sessions;

namespace PCONTB.Panel.Application.Contracts.Application.Services.Auth
{
    public interface ISessionService
    {
        Task<Session?> GetByIdAsync(Guid sessionId);
    }
}

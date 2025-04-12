using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Contracts.Application.Services.Auth
{
    public interface ISessionAccesor
    {
        Session Session { get; }

        public void SetSession(Session session);
    }
}

using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Domain.Account.Sessions;

namespace PCONTB.Panel.Application.Services.Auth
{
    public class SessionAccesor : ISessionAccesor
    {
        public Session Session { get; private set; }

        public void SetSession(Session session)
        {
            Session = session;
        }
    }
}

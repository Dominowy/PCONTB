using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Services.Auth
{
    public class SessionAccesor : ISessionAccesor
    {
        public Session Session { get; private set; }

        public void SetSession(Session session)
        {
            Session = session;
        }

        public void ClearSession()
        {
            Session = null;
        }

        public void Verify(Guid id)
        {
            if (Session.User.Id != id) 
                throw new UnauthorizedException(ErrorCodes.User.AccesDenied.Message);
        }

        public void VerifyWithRole(Guid id)
        {
            if (Session.User.UserRoles.Any(r => r.Role == Role.Admin)) return;

            if (Session.User.Id != id)
                throw new UnauthorizedException(ErrorCodes.User.AccesDenied.Message);
        }
    }
}

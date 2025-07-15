using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
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
                throw new UnauthorizedException(ErrorCodes.Users.User.AccesDenied.Message);
        }

        public void VerifyWithRoles(Guid id, List<Role> roles)
        {
            if (Session.User.UserRoles.Any(r => roles.Contains(r.Role))) return;

            if (Session.User.Id != id)
                throw new UnauthorizedException(ErrorCodes.Users.User.AccesDenied.Message);
        }
    }
}

using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users.Roles;

namespace PCONTB.Panel.Application.Contracts.Services.Auth
{
    public interface ISessionAccesor
    {
        Session Session { get; }
        public void SetSession(Session session);
        public void ClearSession();
        public void Verify(Guid id);
        void VerifyWithRoles(Guid id, List<Role> roles);
    }
}

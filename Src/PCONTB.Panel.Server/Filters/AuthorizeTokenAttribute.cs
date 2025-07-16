using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Infrastructure.Security.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeToken(params Role[] roles) : Attribute, IAuthorizationFilter
    {
        private readonly string cookieName = "access-token";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookieService = context.HttpContext.RequestServices.GetRequiredService<ICookieService>();
            var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();
            var sessionAccesor = context.HttpContext.RequestServices.GetRequiredService<ISessionAccesor>();

            var token = cookieService.Get(cookieName);

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var sessionId = jwtService.GetSessionIdFromToken(token);

            if (sessionId is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var session = sessionAccesor.Session;

            if (session != null)
            {
                var userRoles = session.User.UserRoles.Select(r => r.Role);

                if (!userRoles.Any(r => roles.Contains(r)))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
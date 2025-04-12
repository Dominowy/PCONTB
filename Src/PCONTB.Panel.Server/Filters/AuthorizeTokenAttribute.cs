using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;

namespace PCONTB.Panel.Infrastructure.Security.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeToken : Attribute, IAuthorizationFilter
    {
        private readonly string cookieName = "access-token";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookieService = context.HttpContext.RequestServices.GetRequiredService<ICookieService>();
            var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();

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


        }
    }
}

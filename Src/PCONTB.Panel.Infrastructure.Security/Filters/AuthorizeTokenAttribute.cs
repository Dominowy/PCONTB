using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;

namespace PCONTB.Panel.Infrastructure.Security.Filters
{
    public class AuthorizeTokenAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string cookieName = "access-token";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookieService = context.HttpContext.RequestServices.GetRequiredService<ICookieService>();

            var token = cookieService.Get(cookieName);

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }
    }
}

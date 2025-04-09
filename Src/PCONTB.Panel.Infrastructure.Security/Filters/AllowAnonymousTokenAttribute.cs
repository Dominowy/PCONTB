using Microsoft.AspNetCore.Mvc.Filters;

namespace PCONTB.Panel.Infrastructure.Security.Filters
{
    public class AllowAnonymousTokenAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;

namespace PCONTB.Panel.Server.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AllowAnonymousTokenAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}

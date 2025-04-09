using Microsoft.AspNetCore.Http;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;

namespace PCONTB.Panel.Infrastructure.Security.Auth
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _accessor;

        public CookieService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void Set(string value, string name)
        {
            _accessor.HttpContext.Response.Cookies.Append(name, value, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        public string Get(string name)
        {
            return _accessor.HttpContext.Request.Cookies[name];
        }

        public void Clear(string name)
        {
            _accessor.HttpContext.Response.Cookies.Delete(name);
        }
    }
}

using PCONTB.Panel.Application.Contracts.Services.Auth;

namespace PCONTB.Panel.Server.Services
{
    public class CookieService(IHttpContextAccessor accessor) : ICookieService
    {
        public void Set(string value, string name)
        {
            accessor.HttpContext.Response.Cookies.Append(name, value, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        public string Get(string name)
        {
            return accessor.HttpContext.Request.Cookies[name];
        }

        public void Clear(string name)
        {
            accessor.HttpContext.Response.Cookies.Delete(name);
        }
    }
}

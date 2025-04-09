namespace PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth
{
    public interface ICookieService 
    {
        void Set(string value, string name);
        string Get(string name);
        void Clear(string name);
    }
}

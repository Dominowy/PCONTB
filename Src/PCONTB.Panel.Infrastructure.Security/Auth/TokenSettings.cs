namespace PCONTB.Panel.Infrastructure.Security.Auth
{
    public class TokenSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireMinutes { get; set; }
        public string JwtIssuer { get; set; }
    }
}

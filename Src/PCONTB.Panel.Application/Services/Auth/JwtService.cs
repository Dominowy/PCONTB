using Microsoft.IdentityModel.Tokens;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Infrastructure.Security.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PCONTB.Panel.Application.Services.Auth
{
    public class JwtService(TokenSettings settings) : IJwtService
    {
        public string GenerateToken(Guid sessionId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, sessionId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: settings.JwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(settings.JwtExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (token == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(settings.JwtKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = settings.JwtIssuer,
                }, out var validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Invalid token.", ex);
            }
        }

        public bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                var tokenTime = jwtToken.ValidTo;
                var now = DateTime.UtcNow;

                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }

        public Guid? GetSessionIdFromToken(string token)
        {
            var principal = ValidateToken(token);
            if (principal is null) return null;

            var sessionClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (sessionClaim is null) return null;

            return Guid.TryParse(sessionClaim.Value, out var sessionId)
                ? sessionId
                : null;
        }
    }
}
using System.Security.Claims;

namespace PCONTB.Panel.Application.Contracts.Auth
{
    public interface IJwtService
    {
        string GenerateToken(Guid sessionId);
        bool IsTokenExpired(string token);
        Guid? GetSessionIdFromToken(string token);
    }
}

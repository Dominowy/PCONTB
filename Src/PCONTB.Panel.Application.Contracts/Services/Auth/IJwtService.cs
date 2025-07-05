namespace PCONTB.Panel.Application.Contracts.Services.Auth
{
    public interface IJwtService
    {
        string GenerateToken(Guid sessionId);

        bool IsTokenExpired(string token);

        Guid? GetSessionIdFromToken(string token);
    }
}
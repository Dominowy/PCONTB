using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;

namespace PCONTB.Panel.Server.Middleware
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _cookieName = "access-token";

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IJwtService jwtService,
            ISessionService sessionService,
            ICookieService cookieService,
            ISessionAccesor accesor)
        {
            CancellationToken cancellationToken = context.RequestAborted;
            var token = cookieService.Get(_cookieName);

            if (string.IsNullOrEmpty(token))
            {
                await _next(context);
                return;
            }

            var isExpired = jwtService.IsTokenExpired(token);
            var sessionId = jwtService.GetSessionIdFromToken(token);

            if (sessionId is null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var session = await sessionService.GetByIdAsync(sessionId.Value, cancellationToken);
            if (session is null || !session.IsActive)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (isExpired)
            {
                var newToken = jwtService.GenerateToken(session.Id);
                cookieService.Set(_cookieName, newToken);
            }

            if (session != null)
            {
                accesor.SetSession(session);
            }

            await _next(context);
        }
    }
}

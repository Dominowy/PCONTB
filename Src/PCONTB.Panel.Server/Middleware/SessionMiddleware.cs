using PCONTB.Panel.Application.Contracts.Services.Auth;

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
            if (session is null || !session.Enabled)
            {
                cookieService.Clear(_cookieName);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (await sessionService.CheckSessionActiveState(session, cancellationToken))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (isExpired)
            {
                var newToken = jwtService.GenerateToken(session.Id);
                cookieService.Set(newToken, _cookieName);
            }

            if (session != null)
            {
                accesor.SetSession(session);
            }

            await _next(context);
        }
    }
}
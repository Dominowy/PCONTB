using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;
using PCONTB.Panel.Domain.Account.Sessions;
using System.Security.Claims;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LogoutUserRequest : IRequest<SessionResult>
    {
    }

    public class LogoutUserHandler : IRequestHandler<LogoutUserRequest, SessionResult>
    {
        private readonly string cookieName = "access-token";
        private readonly ICookieService _cookieService;
        private readonly IJwtService _jwtService;
        private readonly IApplicationDbContext _dbContext;

        public LogoutUserHandler(ICookieService cookieService, IJwtService jwtService, IApplicationDbContext dbContext)
        {
            _cookieService = cookieService;
            _jwtService = jwtService;
            _dbContext = dbContext;
        }

        public async Task<SessionResult> Handle(LogoutUserRequest request, CancellationToken cancellationToken)
        {
            var token = _cookieService.Get(cookieName);

            if (string.IsNullOrWhiteSpace(token) || _jwtService.IsTokenExpired(token))
                return new SessionResult();

            var sessionId = _jwtService.GetSessionIdFromToken(token);
            if (sessionId is null)
                return new SessionResult();

            var session = await _dbContext.Set<Session>()
                .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);

            if (session is null || !session.IsActive)
                return new SessionResult();

            session.EndSession();
            await _dbContext.SaveChangesAsync(cancellationToken);

            _cookieService.Clear(cookieName);

            return new SessionResult();
        }

    }
}

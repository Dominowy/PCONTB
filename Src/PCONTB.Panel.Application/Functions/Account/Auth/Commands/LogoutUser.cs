using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;
using PCONTB.Panel.Application.Services.Auth;
using PCONTB.Panel.Domain.Account.Sessions;
using System.Security.Claims;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LogoutUserRequest : IRequest<CommandResult>
    {
    }

    public class LogoutUserHandler : IRequestHandler<LogoutUserRequest, CommandResult>
    {
        private readonly string cookieName = "access-token";
        private readonly ICookieService _cookieService;
        private readonly IJwtService _jwtService;
        private readonly IApplicationDbContext _dbContext;
        private readonly ISessionService _sessionService;

        public LogoutUserHandler(ICookieService cookieService, IJwtService jwtService, ISessionService sessionService, IApplicationDbContext dbContext)
        {
            _cookieService = cookieService;
            _jwtService = jwtService;
            _sessionService = sessionService;
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(LogoutUserRequest request, CancellationToken cancellationToken)
        {
            var token = _cookieService.Get(cookieName);

            if (string.IsNullOrWhiteSpace(token) || _jwtService.IsTokenExpired(token))
                throw new ForbiddenException("Token expired or not exist");

            var sessionId = _jwtService.GetSessionIdFromToken(token);
            if (sessionId is null)
            {
                _cookieService.Clear(cookieName);
                throw new ForbiddenException("Session not exist");
            }

            var session = await _sessionService.GetByIdAsync(sessionId, cancellationToken);

            if (session is null || !session.IsActive)
            {
                _cookieService.Clear(cookieName);
                throw new ForbiddenException("Session not exist");
            }

            session.EndSession();
            await _dbContext.SaveChangesAsync(cancellationToken);

            _cookieService.Clear(cookieName);

            return new CommandResult(session.Id);
        }

    }
}

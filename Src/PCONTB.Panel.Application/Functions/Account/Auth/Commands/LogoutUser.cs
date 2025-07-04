using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Services.Auth;
using PCONTB.Panel.Domain.Repositories;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;
        private readonly ISessionService _sessionService;

        public LogoutUserHandler(ICookieService cookieService, IJwtService jwtService, ISessionService sessionService, IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor)
        {
            _cookieService = cookieService;
            _jwtService = jwtService;
            _sessionService = sessionService;
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(LogoutUserRequest request, CancellationToken cancellationToken)
        {
            var token = _cookieService.Get(cookieName);

            if (string.IsNullOrWhiteSpace(token) || _jwtService.IsTokenExpired(token))
            {
                _cookieService.Clear(cookieName);
                throw new UnauthorizedException("Token expired or not exist");
            }

            var sessionId = _jwtService.GetSessionIdFromToken(token);
            if (sessionId is null)
            {
                _cookieService.Clear(cookieName);
                throw new UnauthorizedException("Session not exist");
            }

            var session = await _sessionService.GetByIdAsync(sessionId, cancellationToken);

            if (session is null || !session.Enabled)
            {
                _cookieService.Clear(cookieName);
                throw new UnauthorizedException("Session not exist");
            }

            session.EndSession();
            await _unitOfWork.SaveAsync(cancellationToken);

            _cookieService.Clear(cookieName);

            _sessionAccesor.ClearSession();

            return new CommandResult(session.Id);
        }
    }
}

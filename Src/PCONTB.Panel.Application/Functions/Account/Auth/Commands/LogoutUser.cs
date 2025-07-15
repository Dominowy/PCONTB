using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LogoutUserRequest : IRequest<CommandResult>
    {
    }

    public class LogoutUserHandler(
        ICookieService cookieService, 
        IJwtService jwtService, 
        ISessionService sessionService, 
        IUnitOfWork unitOfWork, 
        ISessionAccesor sessionAccesor) 
        : IRequestHandler<LogoutUserRequest, CommandResult>
    {
        private readonly string cookieName = "access-token";

        public async Task<CommandResult> Handle(LogoutUserRequest request, CancellationToken cancellationToken)
        {
            var token = cookieService.Get(cookieName);

            if (string.IsNullOrWhiteSpace(token) || jwtService.IsTokenExpired(token))
            {
                cookieService.Clear(cookieName);
                throw new UnauthorizedException("Token expired or not exist");
            }

            var sessionId = jwtService.GetSessionIdFromToken(token);
            if (sessionId is null)
            {
                cookieService.Clear(cookieName);
                throw new UnauthorizedException("Session not exist");
            }

            var session = await sessionService.GetByIdAsync(sessionId, cancellationToken);

            if (session is null || !session.Enabled)
            {
                cookieService.Clear(cookieName);
                throw new UnauthorizedException("Session not exist");
            }

            session.EndSession();
            await unitOfWork.Save(cancellationToken);

            cookieService.Clear(cookieName);

            sessionAccesor.ClearSession();

            return new CommandResult(session.Id);
        }
    }
}

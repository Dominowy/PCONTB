using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LogoutUserRequest : IRequest<SessionResult>
    {
    }

    public class LogoutUserHandler : IRequestHandler<LogoutUserRequest, SessionResult>
    {
        public async Task<SessionResult> Handle(LogoutUserRequest request, CancellationToken cancellationToken)
        {
            return new SessionResult();
        }
    }
}

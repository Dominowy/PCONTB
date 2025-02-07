using MediatR;

namespace PCONTB.Panel.Application.Functions.Account.Authentication.Commands
{
    public class LogoutUserRequest : IRequest
    {
    }

    public class LogoutUserHandler : IRequestHandler<LogoutUserRequest>
    {
        public async Task Handle(LogoutUserRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}

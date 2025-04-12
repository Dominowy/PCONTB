using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class DeleteAccountRequest : IRequest<CommandResult>
    {
    }

    public class DeleteRequestHandler : IRequestHandler<DeleteAccountRequest, CommandResult>
    {
        public Task<CommandResult> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

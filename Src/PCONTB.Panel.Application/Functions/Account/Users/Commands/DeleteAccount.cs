using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class DeleteAccountRequest : IRequest<DeleteResult>
    {
    }

    public class DeleteRequestHandler : IRequestHandler<DeleteAccountRequest, DeleteResult>
    {
        public Task<DeleteResult> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

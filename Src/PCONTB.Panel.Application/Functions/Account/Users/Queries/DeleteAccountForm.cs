using MediatR;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class DeleteAccountFormRequest : IRequest<DeleteAccountFormResponse>
    {
    }

    public class DeleteAccountFormHandler : IRequestHandler<DeleteAccountFormRequest, DeleteAccountFormResponse>
    {
        public Task<DeleteAccountFormResponse> Handle(DeleteAccountFormRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class DeleteAccountFormResponse
    {
    }
}

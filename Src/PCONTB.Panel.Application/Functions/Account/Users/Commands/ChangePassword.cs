using MediatR;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdatePasswordRequest : IRequest<UpdatePasswordResponse>
    {
    }

    public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, UpdatePasswordResponse>
    {
        public Task<UpdatePasswordResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class UpdatePasswordResponse
    {
    }
}

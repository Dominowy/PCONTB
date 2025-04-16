using MediatR;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdatePasswordRequest : IRequest<UpdatePasswordResponse>
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
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

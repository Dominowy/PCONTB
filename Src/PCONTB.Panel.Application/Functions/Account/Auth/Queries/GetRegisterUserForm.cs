using MediatR;
using PCONTB.Panel.Application.Functions.Account.Auth.Commands;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Queries
{
    public class GetRegisterUserFormRequest : IRequest<GetRegisterUserFormResponse>
    {
    }

    public class GetRegisterFormHandler : IRequestHandler<GetRegisterUserFormRequest, GetRegisterUserFormResponse>
    {
        public async Task<GetRegisterUserFormResponse> Handle(GetRegisterUserFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetRegisterUserFormResponse()
            {
                Form = new RegisterUserRequest()
            });
        }
    }

    public class GetRegisterUserFormResponse
    {
        public RegisterUserRequest Form { get; set; }
    }
}

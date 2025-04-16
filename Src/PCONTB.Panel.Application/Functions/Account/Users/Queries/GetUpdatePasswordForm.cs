using MediatR;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdatePasswordFormRequest : IRequest<GetUpdatePasswordFormResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetUpdatePasswordFormHandler : IRequestHandler<GetUpdatePasswordFormRequest, GetUpdatePasswordFormResponse>
    {
        public async Task<GetUpdatePasswordFormResponse> Handle(GetUpdatePasswordFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetUpdatePasswordFormResponse
            {
                Form = new UpdatePasswordRequest()
            });
        }
    }

    public class GetUpdatePasswordFormResponse
    {
        public UpdatePasswordRequest Form { get; set; }
    }
}

using MediatR;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Application.Common.Functions;


namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdatePasswordFormRequest : BaseQuery, IRequest<GetUpdatePasswordFormResponse>
    {

    }

    public class GetUpdatePasswordFormHandler(ISessionAccesor sessionAccesor) 
        : IRequestHandler<GetUpdatePasswordFormRequest, GetUpdatePasswordFormResponse>
    {
        public async Task<GetUpdatePasswordFormResponse> Handle(GetUpdatePasswordFormRequest request, CancellationToken cancellationToken)
        {
            sessionAccesor.Verify(request.Id);

            return await Task.FromResult(new GetUpdatePasswordFormResponse
            {
                Form = new UpdatePasswordRequest()
                {
                    Id = request.Id
                }
            });
        }
    }

    public class GetUpdatePasswordFormResponse
    {
        public UpdatePasswordRequest Form { get; set; }
    }
}

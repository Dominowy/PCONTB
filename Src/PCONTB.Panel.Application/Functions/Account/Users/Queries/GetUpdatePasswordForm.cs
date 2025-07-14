using MediatR;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Application.Common.Functions;


namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdatePasswordFormRequest : BaseQuery, IRequest<GetUpdatePasswordFormResponse>
    {

    }

    public class GetUpdatePasswordFormHandler : IRequestHandler<GetUpdatePasswordFormRequest, GetUpdatePasswordFormResponse>
    {
        private readonly ISessionAccesor _sessionAccesor;

        public GetUpdatePasswordFormHandler(ISessionAccesor sessionAccesor)
        {
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetUpdatePasswordFormResponse> Handle(GetUpdatePasswordFormRequest request, CancellationToken cancellationToken)
        {
            _sessionAccesor.Verify(request.Id);

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

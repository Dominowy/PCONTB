using MediatR;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Models.Account.Auth;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Queries
{
    public class GetSessionRequest : IRequest<GetSessionResponse>
    {
    }

    public class GetSessionHandler(ISessionAccesor sessionAccesor) 
        : IRequestHandler<GetSessionRequest, GetSessionResponse>
    {
        public async Task<GetSessionResponse> Handle(GetSessionRequest request, CancellationToken cancellationToken)
        {
            var user = sessionAccesor.Session.User;

            return await Task.FromResult(new GetSessionResponse
            {
                User = SessionUserDto.Map(user),
            });
        }
    }

    public class GetSessionResponse
    {
        public SessionUserDto User { get; set; }
    }
}

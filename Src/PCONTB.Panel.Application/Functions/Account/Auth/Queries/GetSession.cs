using MediatR;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Models.Dto.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Queries
{
    public class GetSessionRequest : IRequest<GetSessionResponse>
    {
    }

    public class GetSessionHandler : IRequestHandler<GetSessionRequest, GetSessionResponse>
    {
        private readonly ISessionAccesor _sessionAccesor;

        public GetSessionHandler(ISessionAccesor sessionAccesor)
        {
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetSessionResponse> Handle(GetSessionRequest request, CancellationToken cancellationToken)
        {
            var user = _sessionAccesor.Session.User;

            return await Task.FromResult(new GetSessionResponse
            {
                User = UserDto.Map(user),
            });
        }
    }

    public class GetSessionResponse
    {
        public UserDto User { get; set; }
    }
}

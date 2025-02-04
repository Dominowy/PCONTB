using MediatR;

namespace PCONTB.Panel.Application.Account.User.Queries
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
    }

    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        public GetUserHandler()
        {
            
        }

        public Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetUserResponse
    {

    }
}

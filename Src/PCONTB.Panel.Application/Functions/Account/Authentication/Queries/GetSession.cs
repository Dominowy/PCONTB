using MediatR;
using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Functions.Account.Authentication.Queries
{
    public class GetSessionRequest : IRequest<GetSessionResponse>
    {
    }

    public class GetSessionHandler : IRequestHandler<GetSessionRequest, GetSessionResponse>
    {
        public Task<GetSessionResponse> Handle(GetSessionRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetSessionResponse : BaseResponse
    {
        public GetSessionResponse(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {
        }
    }
}

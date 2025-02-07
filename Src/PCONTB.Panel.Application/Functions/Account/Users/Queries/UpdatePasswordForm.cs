using MediatR;
using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdatePasswordFormRequest : IRequest<GetUpdatePasswordFormResponse>
    {
    }

    public class GetUpdatePasswordFormHandler : IRequestHandler<GetUpdatePasswordFormRequest, GetUpdatePasswordFormResponse>
    {
        public Task<GetUpdatePasswordFormResponse> Handle(GetUpdatePasswordFormRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetUpdatePasswordFormResponse : BaseResponse
    {
    }
}

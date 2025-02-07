using MediatR;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdateUserFormRequest : IRequest<GetUpdateUserFormResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetUpdateUserFormHandler : IRequestHandler<GetUpdateUserFormRequest, GetUpdateUserFormResponse>
    {
        public Task<GetUpdateUserFormResponse> Handle(GetUpdateUserFormRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetUpdateUserFormResponse
    {
    }
}

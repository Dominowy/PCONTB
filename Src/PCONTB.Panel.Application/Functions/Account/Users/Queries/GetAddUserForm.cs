using MediatR;
using PCONTB.Panel.Application.Common.Extension.Helpers;
using PCONTB.Panel.Application.Common.Extensions.Helpers;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetAddUserFormRequest : IRequest<GetAddUserFormResponse>
    {
    }

    public class GetAddUserFormHandler : IRequestHandler<GetAddUserFormRequest, GetAddUserFormResponse>
    {
        public async Task<GetAddUserFormResponse> Handle(GetAddUserFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetAddUserFormResponse
            {
               Form = new AddUserRequest(),
               Roles = EnumHelper.EnumToList<Role>()
            });
        }
    }

    public class GetAddUserFormResponse
    {
        public AddUserRequest Form { get; set; }
        public List<EnumItem> Roles { get; set; }
    }
}

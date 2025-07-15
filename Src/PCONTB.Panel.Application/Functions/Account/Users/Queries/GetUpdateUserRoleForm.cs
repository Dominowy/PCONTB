using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Extensions.Helpers.Enums;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdateUserRoleFormRequest : BaseQuery, IRequest<GetUpdateUserRoleFormResponse>
    {

    }

    public class GetUpdateUserRoleFormHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<GetUpdateUserRoleFormRequest, GetUpdateUserRoleFormResponse>
    {
        public async Task<GetUpdateUserRoleFormResponse> Handle(GetUpdateUserRoleFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            return new GetUpdateUserRoleFormResponse
            {
                Form = new UpdateUserRoleRequest
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Roles = [.. entity.UserRoles.Select(m => m.Role)],
                },
                Roles = EnumHelper.EnumToList<Role>()
            };

        }
    }

    public class GetUpdateUserRoleFormResponse
    {
        public UpdateUserRoleRequest Form { get; set; }
        public List<EnumItem> Roles { get; set; }
    }
}

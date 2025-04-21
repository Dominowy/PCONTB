using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Extension.Helpers;
using PCONTB.Panel.Application.Common.Extensions.Helpers;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUpdateUserRoleFormRequest : BaseQuery, IRequest<GetUpdateUserRoleFormResponse>
    {

    }

    public class GetUpdateUserRoleFormHandler : IRequestHandler<GetUpdateUserRoleFormRequest, GetUpdateUserRoleFormResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUpdateUserRoleFormHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUpdateUserRoleFormResponse> Handle(GetUpdateUserRoleFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.User.NotFound.Message);

            return new GetUpdateUserRoleFormResponse
            {
                Form = new UpdateUserRoleRequest
                {
                    Id = entity.Id,
                    Roles = entity.UserRoles.Select(m => m.Role).ToList(),
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

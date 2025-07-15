using PCONTB.Panel.Application.Common.Functions.Tables;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
using System.Data;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class UserPagedQueryHandler(IUnitOfWork unitOfWork) : PagedQueryHandler<User, UserTableDto>
    {
        public override async Task<PagedResultDto<UserTableDto>> Handle(PagedQueryRequest<UserTableDto> request, CancellationToken cancellationToken)
        {
            IQueryable<User> query = GetQuery();

            SetQuery(query);

            return await base.Handle(request, cancellationToken);
        }

        protected override IQueryable<User> GetQuery()
        {
            return unitOfWork.UserRepository.GetQuery();
        }

        protected override string[] GetGlobalSearchProperties()
        {
            return new[] { "Username", "Email", "UserRoles.Role", "Enabled" };
        }

        protected override UserTableDto MapEntityToDto(User user)
        {
            return new UserTableDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Enabled = user.Enabled,
                UserRoles = [.. user.UserRoles.Select(m =>  new UserRoleTableDto
                {
                    Name = m.Role.ToString()
                })]
            };
        }
    }

}

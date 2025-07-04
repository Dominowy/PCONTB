using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Application.Table;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
using System.Data;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class UserPagedQueryHandler : PagedQueryHandler<User, UserTableDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserPagedQueryHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<PagedResultDto<UserTableDto>> Handle(PagedQueryRequest<UserTableDto> request, CancellationToken cancellationToken)
        {
            IQueryable<User> query = GetQuery();

            SetQuery(query);

            return await base.Handle(request, cancellationToken);
        }

        protected override IQueryable<User> GetQuery()
        {
            return _unitOfWork.UserRepository.GetQuery();
        }

        protected override string[] GetGlobalSearchProperties()
        {
            return new[] { "Username", "Email", "UserRoles.Role" };
        }

        protected override UserTableDto MapEntityToDto(User user)
        {
            return new UserTableDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                UserRoles = [.. user.UserRoles.Select(m =>  new UserRoleTableDto
                {
                    Name = m.Role.ToString()
                })]
            };
        }
    }

}

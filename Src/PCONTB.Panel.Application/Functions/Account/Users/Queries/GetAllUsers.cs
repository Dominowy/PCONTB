using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Dto.Account.Users;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetAllUsersRequest : IRequest<GetAllUsersResponse>
    {
    }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllUsersHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>().ToListAsync(cancellationToken);

            return new GetAllUsersResponse
            {
                Users = [.. entity.Select(UserDto.Map)]
            };
        }
    }

    public class GetAllUsersResponse
    {
        public List<UserDto> Users { get; set; }
    }
}

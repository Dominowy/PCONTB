using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Queries
{
    public class GetUserRequest : BaseQuery, IRequest<GetUserResponse>
    {
    }

    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISessionAccesor _sessionAccesor;

        public GetUserHandler(IApplicationDbContext context, ISessionAccesor sessionAccesor)
        {
            _context = context;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<User>()
                .Include(m => m.UserRoles)
                .Include(m => m.Projects)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("User not found");

            _sessionAccesor.Verify(entity.Id);

            return new GetUserResponse()
            {
                User = UserDto.Map(entity),
            };
        }
    }

    public class GetUserResponse
    {
        public UserDto User { get; set; }
    }
}
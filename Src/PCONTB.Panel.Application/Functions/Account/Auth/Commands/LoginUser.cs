using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.DbContext;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LoginUserRequest : IRequest<SessionResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserHandler : IRequestHandler<LoginUserRequest, SessionResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public LoginUserHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SessionResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password, cancellationToken);

            if (entity == null) 
            {
                return new SessionResult(false, ResponseStatus.BadRequest);
            }

            return new SessionResult()
            {
                UserId = entity.Id
            };
        }
    }
}

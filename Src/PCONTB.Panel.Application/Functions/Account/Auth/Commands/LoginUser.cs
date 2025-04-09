using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth.Encryption;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LoginUserRequest : IRequest<SessionResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserHandler : IRequestHandler<LoginUserRequest, SessionResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJwtService _jwtService;
        private readonly ICookieService _cookieService;

        public LoginUserHandler(IApplicationDbContext dbContext, 
            IPasswordHasherService passwordHasherService, 
            IJwtService jwtService,
            ICookieService cookieService)
        {
            _dbContext = dbContext;
            _passwordHasherService = passwordHasherService;
            _jwtService = jwtService;
            _cookieService = cookieService;
        }

        public async Task<SessionResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == request.Login || u.Username == request.Login, cancellationToken);

            if (entity == null) 
            {
                return new SessionResult(false, ResponseStatus.BadRequest);
            }

            if (!_passwordHasherService.Verify(request.Password, entity.Password))
            {
                return new SessionResult(false, ResponseStatus.BadRequest);
            }

            var session = new Session(Guid.NewGuid(), entity.Id);

            await _dbContext.Set<Session>().AddAsync(session, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var token = _jwtService.GenerateToken(session.Id);

            _cookieService.Set(token, "access-token");

            return new SessionResult();
        }
    }
}

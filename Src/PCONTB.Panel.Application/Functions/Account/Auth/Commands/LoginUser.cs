using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth.Encryption;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LoginUserRequest : IRequest<CommandResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserHandler : IRequestHandler<LoginUserRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJwtService _jwtService;
        private readonly ICookieService _cookieService;
        private readonly ISessionService _sessionService;

        public LoginUserHandler(IApplicationDbContext dbContext, 
            IPasswordHasherService passwordHasherService, 
            IJwtService jwtService,
            ICookieService cookieService,
            ISessionService sessionService)
        {
            _dbContext = dbContext;
            _passwordHasherService = passwordHasherService;
            _jwtService = jwtService;
            _cookieService = cookieService;
            _sessionService = sessionService;
        }

        public async Task<CommandResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == request.Login || u.Username == request.Login, cancellationToken);

            if (entity == null) throw new BadRequestException(ErrorCodes.User.LoginWrongCredential.Message);

            if (!_passwordHasherService.Verify(request.Password, entity.Password)) 
                throw new BadRequestException(ErrorCodes.User.LoginWrongCredential.Message);

            var sessionId = await _sessionService.CreateSession(entity.Id, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var token = _jwtService.GenerateToken(sessionId);

            _cookieService.Set(token, "access-token");

            return new CommandResult(sessionId);
        }
    }
}

using FluentValidation;
using MediatR;
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
    public class RegisterUserRequest : IRequest<CommandResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterHandler : IRequestHandler<RegisterUserRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJwtService _jwtService;
        private readonly ICookieService _cookieService;
        private readonly ISessionService _sessionService;

        public RegisterHandler(IApplicationDbContext dbContext, IPasswordHasherService passwordHasherService, IJwtService jwtService, ICookieService cookieService, ISessionService sessionService)
        {
            _dbContext = dbContext;
            _passwordHasherService = passwordHasherService;
            _jwtService = jwtService;
            _cookieService = cookieService;
            _sessionService = sessionService;
        }

        public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasherService.Generate(request.Password);

            var entity = new User(Guid.NewGuid(), request.Username, request.Email, hashedPassword);

            await _dbContext.Set<User>().AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var sessionId = await _sessionService.CreateSession(entity.Id, cancellationToken);

            var token = _jwtService.GenerateToken(sessionId);

            _cookieService.Set(token, "access-token");

            return new CommandResult(sessionId);
        }
    }

    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithErrorCode(ErrorCodes.Project.ProjectNameEmpty.Code);

            RuleFor(p => p.Email)
                .NotEmpty().WithErrorCode(ErrorCodes.Project.ProjectNameEmpty.Code);
        }
    }
}

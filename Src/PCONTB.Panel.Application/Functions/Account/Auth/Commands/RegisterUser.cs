using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth.Encryption;
using PCONTB.Panel.Domain.Account.Users;
using System.Text.RegularExpressions;

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
        private readonly IApplicationDbContext _dbContext;

        public RegisterUserValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(p => p.Username)
                .NotNull().WithMessage(ErrorCodes.User.UsernameEmpty.Message)
                .NotEmpty().WithMessage(ErrorCodes.User.UsernameEmpty.Message)
                .MustAsync(CheckUsernameIsUnique).WithMessage(ErrorCodes.User.UsernameExist.Message); ;

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ErrorCodes.User.EmailEmpty.Message)
                .NotEmpty().WithMessage(ErrorCodes.User.EmailEmpty.Message)
                .EmailAddress().WithMessage(ErrorCodes.User.EmailBadFormat.Message)
                .MustAsync(CheckEmailIsUnique).WithMessage(ErrorCodes.User.EmailExist.Message);

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(ErrorCodes.User.PasswordEmpty.Message)
                .NotEmpty().WithMessage(ErrorCodes.User.PasswordEmpty.Message)
                .MinimumLength(8).WithMessage(ErrorCodes.User.PasswordMinimalLength.Message)
                .Matches(new Regex(@"(?=[A-Za-z0-9@#$%^&+-_!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+-_!=]).*$"))
                .WithMessage(ErrorCodes.User.PasswordMatchRules.Message);
        }

        private async Task<bool> CheckUsernameIsUnique(string username, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<User>().AnyAsync(m => m.Username == username, cancellationToken);
        }

        private async Task<bool> CheckEmailIsUnique(string email, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<User>().AnyAsync(m => m.Email == email, cancellationToken);
        }

    }
}

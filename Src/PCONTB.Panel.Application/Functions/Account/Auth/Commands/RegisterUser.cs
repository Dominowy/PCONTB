using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Contracts.Services.Auth.Encryption;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJwtService _jwtService;
        private readonly ICookieService _cookieService;
        private readonly ISessionService _sessionService;

        public RegisterHandler(IUnitOfWork unitOfWork, IPasswordHasherService passwordHasherService, IJwtService jwtService, ICookieService cookieService, ISessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasherService = passwordHasherService;
            _jwtService = jwtService;
            _cookieService = cookieService;
            _sessionService = sessionService;
        }

        public async Task<CommandResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasherService.Generate(request.Password);

            var entity = new User(Guid.NewGuid(), request.Username, request.Email, hashedPassword);

            var role = new UserRole(Role.User, entity.Id);

            entity.UserRoles.Add(role);

            await _unitOfWork.UserRepository.Add(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            var sessionId = await _sessionService.CreateSession(entity.Id, cancellationToken);

            var token = _jwtService.GenerateToken(sessionId);

            _cookieService.Set(token, "access-token");

            return new CommandResult(sessionId);
        }
    }

    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage(ErrorCodes.User.UsernameEmpty.Message)
                .MustAsync(CheckUsernameIsUnique).WithMessage(ErrorCodes.User.UsernameExist.Message);

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ErrorCodes.User.EmailEmpty.Message)
                .EmailAddress().WithMessage(ErrorCodes.User.EmailBadFormat.Message)
                .MustAsync(CheckEmailIsUnique).WithMessage(ErrorCodes.User.EmailExist.Message);

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(ErrorCodes.User.PasswordEmpty.Message)
                .MinimumLength(8).WithMessage(ErrorCodes.User.PasswordMinimalLength.Message)
                .Matches(new Regex(@"(?=[A-Za-z0-9@#$%^&+-_!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+-_!=]).*$"))
                .WithMessage(ErrorCodes.User.PasswordMatchRules.Message);
        }

        private async Task<bool> CheckUsernameIsUnique(string username, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.UserRepository.Exist(m => m.Username == username, cancellationToken);
        }

        private async Task<bool> CheckEmailIsUnique(string email, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.UserRepository.Exist(m => m.Email == email, cancellationToken);
        }
    }
}
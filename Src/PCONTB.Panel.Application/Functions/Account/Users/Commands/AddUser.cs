using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth.Encryption;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Account.Users;
using System.Text.RegularExpressions;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class AddUserRequest : IRequest<CommandResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
    }

    public class AddUserHandler : IRequestHandler<AddUserRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasherService _passwordHasherService;

        public AddUserHandler(IApplicationDbContext dbContext, IPasswordHasherService passwordHasherService)
        {
            _dbContext = dbContext;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<CommandResult> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasherService.Generate(request.Password);

            var entity = new User(Guid.NewGuid(), request.Username, request.Email, hashedPassword);

            await _dbContext.Set<User>().AddAsync(entity, cancellationToken);

            var roles = request.Roles.Select(m => new UserRole(m, entity.Id)).ToList();

            _dbContext.Set<UserRole>().AddRange(roles);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddUserValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

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

            RuleFor(p => p.Roles)
                .NotEmpty().WithMessage(ErrorCodes.User.RoleEmpty.Message)
                .Must(NoDuplicates).WithMessage(ErrorCodes.User.RoleDuplicate.Message)
                .Must(AllRolesValid).WithMessage(ErrorCodes.User.RoleValid.Message);
        }

        private async Task<bool> CheckUsernameIsUnique(string username, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<User>().AnyAsync(m => m.Username == username, cancellationToken);
        }

        private async Task<bool> CheckEmailIsUnique(string email, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<User>().AnyAsync(m => m.Email == email, cancellationToken);
        }

        private bool NoDuplicates(List<Role> roles)
        {
            return roles.Distinct().Count() == roles.Count;
        }

        private bool AllRolesValid(List<Role> roles)
        {
            var validValues = Enum.GetValues(typeof(Role)).Cast<Role>();
            return roles.All(r => validValues.Contains(r));
        }
    }
}

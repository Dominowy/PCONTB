using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth.Encryption;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Users;
using System.Text.RegularExpressions;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdatePasswordRequest : BaseCommand, IRequest<CommandResult>
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, CommandResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISessionAccesor _sessionAccesor;
        private readonly IPasswordHasherService _passwordHasherService;

        public UpdatePasswordHandler(IApplicationDbContext context, IPasswordHasherService passwordHasherService, ISessionAccesor sessionAccesor)
        {
            _passwordHasherService = passwordHasherService;
            _context = context;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasherService.Generate(request.Password);

            var entity = await _context.Set<User>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("User not found");

            _sessionAccesor.Verify(entity.Id);

            entity.SetPassword(hashedPassword);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequest>
    {

        public UpdatePasswordRequestValidator()
        {
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(ErrorCodes.User.PasswordEmpty.Message)
                .MinimumLength(8).WithMessage(ErrorCodes.User.PasswordMinimalLength.Message)
                .Matches(new Regex(@"(?=[A-Za-z0-9@#$%^&+-_!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+-_!=]).*$"))
                .WithMessage(ErrorCodes.User.PasswordMatchRules.Message);

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage(ErrorCodes.User.PasswordEmpty.Message)
                .Equal(u => u.Password).WithMessage(ErrorCodes.User.PasswordsNotEqual.Message);
        }
    }
}

using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Contracts.Services.Auth.Encryption;
using PCONTB.Panel.Domain.Repositories;
using System.Text.RegularExpressions;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdatePasswordRequest : BaseCommand, IRequest<CommandResult>
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UpdatePasswordHandler(
        IUnitOfWork unitOfWork, 
        IPasswordHasherService passwordHasherService, 
        ISessionAccesor sessionAccesor) 
        : IRequestHandler<UpdatePasswordRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = passwordHasherService.Generate(request.Password);

            var entity = await unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException("User not found");

            sessionAccesor.Verify(entity.Id);

            entity.SetPassword(hashedPassword);

            await unitOfWork.UserRepository.Update(entity, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequest>
    {

        public UpdatePasswordRequestValidator()
        {
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(ErrorCodes.Users.User.PasswordEmpty.Message)
                .MinimumLength(8).WithMessage(ErrorCodes.Users.User.PasswordMinimalLength.Message)
                .Matches(new Regex(@"(?=[A-Za-z0-9@#$%^&+-_!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+-_!=]).*$"))
                .WithMessage(ErrorCodes.Users.User.PasswordMatchRules.Message);

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage(ErrorCodes.Users.User.PasswordEmpty.Message)
                .Equal(u => u.Password).WithMessage(ErrorCodes.Users.User.PasswordsNotEqual.Message);
        }
    }
}

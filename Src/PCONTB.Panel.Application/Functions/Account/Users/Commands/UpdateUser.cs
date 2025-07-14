using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRequest : BaseCommand, IRequest<CommandResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;

        public UpdateUserHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor)
        {
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("User not found");

            _sessionAccesor.Verify(entity.Id);

            entity.SetEmail(request.Email);
            entity.SetUsername(request.Username);

            await _unitOfWork.UserRepository.Update(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddProjectRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddProjectRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage(ErrorCodes.Users.User.UsernameEmpty.Message)
                .MustAsync(async (s, u, ct) => await CheckUsernameIsUnique(s.Id, u, ct))
                .WithMessage(ErrorCodes.Users.User.UsernameExist.Message);

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ErrorCodes.Users.User.EmailEmpty.Message)
                .EmailAddress().WithMessage(ErrorCodes.Users.User.EmailBadFormat.Message)
                .MustAsync(async (s, u, ct) => await CheckEmailIsUnique(s.Id, u, ct)).WithMessage(ErrorCodes.Users.User.EmailExist.Message);
        }

        private async Task<bool> CheckUsernameIsUnique(Guid id, string username, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.UserRepository.Exist(m => m.Username == username && m.Id != id, cancellationToken);
        }

        private async Task<bool> CheckEmailIsUnique(Guid id, string email, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.UserRepository.Exist(m => m.Email == email && m.Id != id, cancellationToken);
        }
    }
}

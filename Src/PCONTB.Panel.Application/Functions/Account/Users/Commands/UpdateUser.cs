using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRequest : BaseCommand, IRequest<CommandResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, CommandResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISessionAccesor _sessionAccesor;

        public UpdateUserHandler(IApplicationDbContext context, ISessionAccesor sessionAccesor)
        {
            _context = context;
            _sessionAccesor = sessionAccesor;
        }

        public async Task<CommandResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<User>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("User not found");

            _sessionAccesor.Verify(entity.Id);

            entity.SetEmail(request.Email);
            entity.SetUsername(request.Username);

            await _context.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddProjectRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddProjectRequestValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage(ErrorCodes.User.UsernameEmpty.Message)
                .MustAsync(async (s, u, ct) => await CheckUsernameIsUnique(s.Id, u, ct))
                .WithMessage(ErrorCodes.User.UsernameExist.Message);

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(ErrorCodes.User.EmailEmpty.Message)
                .EmailAddress().WithMessage(ErrorCodes.User.EmailBadFormat.Message)
                .MustAsync(async (s, u, ct) => await CheckEmailIsUnique(s.Id, u, ct)).WithMessage(ErrorCodes.User.EmailExist.Message);
        }

        private async Task<bool> CheckUsernameIsUnique(Guid id, string username, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<User>().AnyAsync(m => m.Username == username && m.Id != id, cancellationToken);
        }

        private async Task<bool> CheckEmailIsUnique(Guid id, string email, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<User>().AnyAsync(m => m.Email == email && m.Id != id, cancellationToken);
        }
    }
}

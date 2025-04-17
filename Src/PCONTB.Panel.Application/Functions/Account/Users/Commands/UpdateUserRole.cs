using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRoleRequest : BaseCommand, IRequest<CommandResult>
    {
        public Role Role { get; set; }
    }

    public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateUserRoleHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(UpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.User.NotFound.Message);

            entity.SetRole(request.Role);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(p => p.Role)
                .NotEmpty().WithMessage(ErrorCodes.User.RoleEmpty.Message);
        }
    }
}

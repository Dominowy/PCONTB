using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRoleRequest : IRequest<CommandResult>
    {
        public Guid Id { get; set; }

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

            entity.UpdateRole(request.Role);

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

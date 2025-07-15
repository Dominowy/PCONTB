using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
using System.Data;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRoleRequest : BaseCommand, IRequest<CommandResult>
    {
        public string Username { get; set; }
        public List<Role> Roles { get; set; } = [];
    }

    public class UpdateUserRoleHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserRoleRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(UpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.UserRepository.GetByTracking(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            if (request.Roles.Any(m => m == Role.Block))
            {
                entity.SetEnabled(false);
            } 
            else
            {
                entity.SetEnabled(true);
            }

            var rolesToRemove = entity.UserRoles.Where(m => !request.Roles.Contains(m.Role)).ToList();

            entity.UserRoles.RemoveAll(m => !request.Roles.Contains(m.Role));

            var rolesToAdd = request.Roles.Where(m => !entity.UserRoles.Any(x => x.Role == m))
                              .Select(m => new UserRole(m, entity.Id))
                              .ToList();

            entity.UserRoles.AddRange(rolesToAdd);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(p => p.Roles)
                .NotEmpty().WithMessage(ErrorCodes.Users.User.RoleEmpty.Message)
                .Must(NoDuplicates).WithMessage(ErrorCodes.Users.User.RoleDuplicate.Message)
                .Must(AllRolesValid).WithMessage(ErrorCodes.Users.User.RoleValid.Message);
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

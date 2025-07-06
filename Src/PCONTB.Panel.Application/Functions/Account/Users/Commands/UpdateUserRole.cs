using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
using System.Data;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRoleRequest : BaseCommand, IRequest<CommandResult>
    {
        public List<Role> Roles { get; set; }
    }

    public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(UpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.User.NotFound.Message);

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

            await _unitOfWork.UserRepository.Update(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(p => p.Roles)
                .NotEmpty().WithMessage(ErrorCodes.User.RoleEmpty.Message)
                .Must(NoDuplicates).WithMessage(ErrorCodes.User.RoleDuplicate.Message)
                .Must(AllRolesValid).WithMessage(ErrorCodes.User.RoleValid.Message);
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

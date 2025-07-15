using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UnlockUserRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class UnlockUserHandler : IRequestHandler<UnlockUserRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnlockUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(UnlockUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserRepository.GetByTracking(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            entity.SetEnabled(true);

            var roleBlock = entity.UserRoles.FirstOrDefault(m => m.Role == Role.Block);

            if (roleBlock is null) throw new NotFoundException(ErrorCodes.Users.User.UserIsNotBlock.Message);

            entity.UserRoles.Remove(roleBlock);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Domain.Account.Users.Roles;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UnlockUserRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class UnlockUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<UnlockUserRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(UnlockUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.UserRepository.GetByTracking(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            entity.SetEnabled(true);

            var roleBlock = entity.Roles.FirstOrDefault(m => m.Role == Role.Block) 
                ?? throw new NotFoundException(ErrorCodes.Users.User.UserIsNotBlock.Message);

            entity.Roles.Remove(roleBlock);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Account.Users.Roles;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class LockUserRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class LockUserHandler(
        IUnitOfWork unitOfWork, 
        ISessionAccesor sessionAccesor, 
        ISessionService sessionService) 
        : IRequestHandler<LockUserRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(LockUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.UserRepository.GetByTracking(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Users.User.NotFound.Message);

            sessionAccesor.VerifyWithRoles(entity.Id, [Role.Admin, Role.Moderator]);

            entity.SetEnabled(false);

            var blockRole = new UserRole(Role.Block, entity.Id);

            entity.Roles.Add(blockRole);

            await sessionService.EndAllSession(entity.Id, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}
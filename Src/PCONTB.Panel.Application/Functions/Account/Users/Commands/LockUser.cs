using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class LockUserRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class LockUserHandler : IRequestHandler<LockUserRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionAccesor _sessionAccesor;
        private readonly ISessionService _sessionService;


        public LockUserHandler(IUnitOfWork unitOfWork, ISessionAccesor sessionAccesor, ISessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _sessionAccesor = sessionAccesor;
            _sessionService = sessionService;
        }

        public async Task<CommandResult> Handle(LockUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.User.NotFound.Message);

            _sessionAccesor.VerifyWithRole(entity.Id, Role.Admin);

            entity.SetEnabled(false);

            var blockRole = new UserRole(Role.Block, entity.Id);

            entity.UserRoles.Add(blockRole);

            await _sessionService.EndAllSession(entity.Id, cancellationToken);

            await _unitOfWork.UserRepository.Update(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}
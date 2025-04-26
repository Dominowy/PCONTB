using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;
using PCONTB.Panel.Application.Services.Auth;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class LockUserRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class LockUserHandler : IRequestHandler<LockUserRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ISessionAccesor _sessionAccesor;
        private readonly ISessionService _sessionService;


        public LockUserHandler(IApplicationDbContext dbContext, ISessionAccesor sessionAccesor, ISessionService sessionService)
        {
            _dbContext = dbContext;
            _sessionAccesor = sessionAccesor;
            _sessionService = sessionService;
        }

        public async Task<CommandResult> Handle(LockUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.User.NotFound.Message);

            _sessionAccesor.VerifyWithRole(entity.Id, Role.Admin);

            var blockRole = new UserRole(Role.Block, entity.Id);

            await _dbContext.Set<UserRole>().AddAsync(blockRole, cancellationToken);

            await _sessionService.EndAllSession(entity.Id, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}
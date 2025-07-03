using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UnlockUserRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class UnlockUserHandler : IRequestHandler<UnlockUserRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;


        public UnlockUserHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(UnlockUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<User>()
                .Include(m => m.UserRoles)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.User.NotFound.Message);

            entity.SetEnabled(true);

            var roleBlock = entity.UserRoles.FirstOrDefault(m => m.Role == Role.Block);

            if (roleBlock is null) throw new NotFoundException(ErrorCodes.User.UserIsNotBlock.Message);

            _dbContext.Set<UserRole>().Remove(roleBlock);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}
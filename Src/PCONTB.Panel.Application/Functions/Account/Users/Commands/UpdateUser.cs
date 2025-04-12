using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRequest : IRequest<CommandResult>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, CommandResult>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CommandResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<User>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException("User not found");

            entity.ChangeEmail(request.Email);
            entity.ChangeUsername(request.Username);

            return new CommandResult(entity.Id);
        }
    }
}

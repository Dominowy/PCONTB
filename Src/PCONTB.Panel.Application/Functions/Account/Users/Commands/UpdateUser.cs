using MediatR;
using PCONTB.Panel.Application.Common.Models.Response;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UpdateUserRequest : IRequest<UpdateResult>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateResult>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UpdateResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<User>().FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                return new UpdateResult(false, ResponseStatus.BadRequest);
            }

            entity.ChangeEmail(request.Email);
            entity.ChangeUsername(request.Username);

            return new UpdateResult();
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.DbContext;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Authentication.Commands
{
    public class RegisterUserRequest : IRequest<CreateResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterHandler : IRequestHandler<RegisterUserRequest, CreateResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public RegisterHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var entity = new User(Guid.NewGuid(), request.Username, request.Email, request.Password);

            await _dbContext.Set<User>().AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateResult();
        }
    }
}

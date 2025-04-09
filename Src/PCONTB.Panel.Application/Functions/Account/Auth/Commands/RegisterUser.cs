using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth.Encryption;
using PCONTB.Panel.Domain.Account.Users;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
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
        private readonly IPasswordHasherService _passwordHasherService;

        public RegisterHandler(IApplicationDbContext dbContext, IPasswordHasherService passwordHasherService)
        {
            _dbContext = dbContext;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<CreateResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasherService.Generate(request.Password);

            var entity = new User(Guid.NewGuid(), request.Username, request.Email, hashedPassword);

            await _dbContext.Set<User>().AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateResult();
        }
    }
}

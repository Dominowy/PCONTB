using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Contracts.Services.Auth.Encryption;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class LoginUserRequest : IRequest<CommandResult>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasherService passwordHasherService,
        IJwtService jwtService,
        ICookieService cookieService,
        ISessionService sessionService) 
        : IRequestHandler<LoginUserRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.UserRepository.GetBy(u => u.Email == request.Login || u.Username == request.Login, cancellationToken) 
                ?? throw new BadRequestException(ErrorCodes.Users.User.LoginWrongCredential.Message);

            if (entity.UserRoles.Any(r => r.Role == Role.Block)) 
                throw new BadRequestException(ErrorCodes.Users.User.AccountLock.Message);

            if (!passwordHasherService.Verify(request.Password, entity.Password))
                throw new BadRequestException(ErrorCodes.Users.User.LoginWrongCredential.Message);

            var sessionId = await sessionService.CreateSession(entity.Id, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            var token = jwtService.GenerateToken(sessionId);

            cookieService.Set(token, "access-token");

            return new CommandResult(sessionId);
        }
    }
}
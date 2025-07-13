using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Result;
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

    public class LoginUserHandler : IRequestHandler<LoginUserRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJwtService _jwtService;
        private readonly ICookieService _cookieService;
        private readonly ISessionService _sessionService;

        public LoginUserHandler(IUnitOfWork unitOfWork,
            IPasswordHasherService passwordHasherService,
            IJwtService jwtService,
            ICookieService cookieService,
            ISessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasherService = passwordHasherService;
            _jwtService = jwtService;
            _cookieService = cookieService;
            _sessionService = sessionService;
        }

        public async Task<CommandResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.UserRepository.GetBy(u => u.Email == request.Login || u.Username == request.Login, cancellationToken);

            if (entity == null) throw new BadRequestException(ErrorCodes.Users.User.LoginWrongCredential.Message);

            if (entity.UserRoles.Any(r => r.Role == Role.Block)) throw new BadRequestException(ErrorCodes.Users.User.AccountLock.Message);

            if (!_passwordHasherService.Verify(request.Password, entity.Password))
                throw new BadRequestException(ErrorCodes.Users.User.LoginWrongCredential.Message);

            var sessionId = await _sessionService.CreateSession(entity.Id, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            var token = _jwtService.GenerateToken(sessionId);

            _cookieService.Set(token, "access-token");

            return new CommandResult(sessionId);
        }
    }
}
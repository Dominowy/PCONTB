using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Account.Auth.Commands;
using PCONTB.Panel.Application.Functions.Account.Auth.Queries;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Account.Auth
{
    [Route("api/account/auth")]
    public class AuthController(IMediator mediator) : BaseController(mediator)
    {
        #region Register

        [AllowAnonymousToken]
        [HttpPost("register/form")]
        [ProducesResponseType(typeof(GetRegisterUserFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] GetRegisterUserFormRequest registerRequest, CancellationToken cancellationToken) => await Send(registerRequest, cancellationToken);

        [AllowAnonymousToken]
        [HttpPost("register")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest, CancellationToken cancellationToken) => await Send(registerRequest, cancellationToken);

        [AllowAnonymousToken]
        [HttpPost("register/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterValidate([FromBody] RegisterUserRequest registerRequest, CancellationToken cancellationToken) => await Validate(registerRequest, cancellationToken);

        #endregion Register

        [AllowAnonymousToken]
        [HttpPost("login")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest, CancellationToken cancellationToken) => await Send(loginRequest, cancellationToken);

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Logout([FromBody] LogoutUserRequest logoutRequest, CancellationToken cancellationToken) => await Send(logoutRequest, cancellationToken);

        [AllowAnonymousToken]
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Reset([FromBody] ResetUserPasswordRequest resetPasswordRequest, CancellationToken cancellationToken) => await Send(resetPasswordRequest, cancellationToken);

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("get-session")]
        [ProducesResponseType(typeof(GetSessionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSession([FromBody] GetSessionRequest getSessionRequest, CancellationToken cancellationToken) => await Send(getSessionRequest, cancellationToken);

    }
}

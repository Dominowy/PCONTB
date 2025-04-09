using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Account.Auth.Commands;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Account.Authentication
{
    [Route("account/auth")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(CreateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest, CancellationToken cancellationToken) => await Send(registerRequest, cancellationToken);

        [HttpPost("login")]
        [ProducesResponseType(typeof(SessionResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest, CancellationToken cancellationToken) => await Send(loginRequest, cancellationToken);

        [HttpPost("logout")]
        [ProducesResponseType(typeof(SessionResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout([FromBody] LogoutUserRequest logoutRequest, CancellationToken cancellationToken) => await Send(logoutRequest, cancellationToken);

        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(UpdateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Reset([FromBody] ResetUserPasswordRequest resetPasswordRequest, CancellationToken cancellationToken) => await Send(resetPasswordRequest, cancellationToken);

    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Account.Authentication.Commands;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Account.Authentication
{
    [Route("authentication")]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(CreateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(registerRequest, cancellationToken);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginRequest, cancellationToken);

            return Ok(result);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutUserRequest logoutRequest, CancellationToken cancellationToken)
        {
            await _mediator.Send(logoutRequest, cancellationToken);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> Reset([FromBody] ResetPasswordRequest logoutRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(logoutRequest, cancellationToken);

            return Ok(result);
        }
    }
}

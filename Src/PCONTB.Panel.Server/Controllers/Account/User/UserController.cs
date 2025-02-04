using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Account.User.Queries;

namespace PCONTB.Panel.Server.Controllers.Account.User
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        public async Task<GetUserResponse> Get()
        {
            return await _mediator.Send(new GetUserRequest());
        }


    }
}

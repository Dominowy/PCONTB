using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Account.Users
{
    [Route("account/users")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }
    }
}

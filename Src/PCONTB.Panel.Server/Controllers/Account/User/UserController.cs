using MediatR;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Account.User
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
    }
}

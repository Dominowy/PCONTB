using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Collaborators
{
    [Route("projects/collaborators")]
    public class CollaboratorsController : BaseController
    {
        public CollaboratorsController(IMediator mediator) : base(mediator)
        {
        }
    }
}

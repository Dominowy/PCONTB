using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Images
{
    [Route("projects/images")]
    public class ImagesController : BaseController
    {
        public ImagesController(IMediator mediator) : base(mediator)
        {
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Images
{
    [Route("projects/images")]
    public class ImagesController(IMediator mediator) : BaseController(mediator)
    {
    }
}

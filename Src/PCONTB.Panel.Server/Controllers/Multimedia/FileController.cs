using MediatR;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Multimedia
{
    public class FileController : BaseController
    {
        public FileController(IMediator mediator) : base(mediator)
        {
        }
    }
}

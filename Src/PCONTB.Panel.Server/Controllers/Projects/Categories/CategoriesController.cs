using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Categories
{
    [Route("projects/categories")]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IMediator mediator) : base(mediator)
        {
        }
    }
}

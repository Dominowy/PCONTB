using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Location.Countries
{
    [Route("location/countries")]
    public class CountriesController : BaseController
    {
        public CountriesController(IMediator mediator) : base(mediator)
        {
        }
    }
}

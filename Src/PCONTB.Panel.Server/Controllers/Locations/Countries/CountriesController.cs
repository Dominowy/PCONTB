using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Common.Models.Select;
using PCONTB.Panel.Application.Functions.Location.Countries.Commands;
using PCONTB.Panel.Application.Functions.Location.Countries.Queries;
using PCONTB.Panel.Application.Functions.Projects.Categories.Queries;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Locations.Countries
{
    [Route("api/locations/countries")]
    public class CountriesController(IMediator mediator) : BaseController(mediator)
    {
        #region Get by id

        [AuthorizeToken(Role.Admin)]
        [HttpPost("get-by-id")]
        [ProducesResponseType(typeof(GetCountryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountryById([FromBody] GetCountryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get by id

        #region Get all

        [AllowAnonymousToken]
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllCountriesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCountries([FromBody] GetAllCountriesRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get all

        #region Add

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddCountryFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCountry([FromBody] GetAddCountryFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateAddCountry([FromBody] AddCountryRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCountry([FromBody] AddCountryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateCountryFormResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCountryForm([FromBody] GetUpdateCountryFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdateCountry([FromBody] UpdateCountryRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Delete

        [AuthorizeToken(Role.Admin)]
        [HttpPost("delete/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateDeleteCountry([FromBody] DeleteCountryRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCountry([FromBody] DeleteCountryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete

        #region Select

        [AuthorizeToken(Role.User)]
        [HttpPost("select-countries")]
        [ProducesResponseType(typeof(SelectResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SelectCountries([FromBody] SelectCountriesRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete
    }
}

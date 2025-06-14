using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Projects.Categories.Queries;
using PCONTB.Panel.Application.Functions.Projects.Files.Images.Commands;
using PCONTB.Panel.Application.Functions.Projects.Files.Images.Queries;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Images
{
    [Route("api/projects/images")]
    public class FilesController(IMediator mediator) : BaseController(mediator)
    {
        #region Get 

        //[AllowAnonymousToken]
        //[HttpPost("get-by-id")]
        //[ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetImageById([FromBody] GetImageRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get by id

        #region Add

        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddImageFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddImage([FromBody] GetAddImageFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateAddImage([FromBody] AddImageRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddImage([FromBody] AddImageRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateImageFormResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateImageForm([FromBody] GetUpdateImageFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdateImage([FromBody] UpdateImageRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateImage([FromBody] UpdateImageRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update
    }
}

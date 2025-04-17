using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Location.Countries.Commands;
using PCONTB.Panel.Application.Functions.Projects.Categories.Commands;
using PCONTB.Panel.Application.Functions.Projects.Categories.Queries;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Categories
{
    [Route("projects/categories")]
    public class CategoriesController(IMediator mediator) : BaseController(mediator)
    {
        #region Get by id

        [AuthorizeToken(Role.Admin)]
        [HttpPost("get-by-id")]
        [ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById([FromBody] GetCategoryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get by id

        #region Get all

        [AllowAnonymousToken]
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllCategoriesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories([FromBody] GetAllCategoriesRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get all

        #region Add

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddCategoryFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCategory([FromBody] GetAddCategoryFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateAddCategory([FromBody] AddCategoryRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateCategoryFormResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategoryForm([FromBody] GetUpdateCategoryFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdateCategory([FromBody] UpdateCategoryRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Delete

        [AuthorizeToken(Role.Admin)]
        [HttpPost("delete/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateDeleteCategory([FromBody] DeleteCategoryRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete
    }
}
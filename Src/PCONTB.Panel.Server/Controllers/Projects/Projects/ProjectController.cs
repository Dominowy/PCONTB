using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands;
using PCONTB.Panel.Application.Functions.Projects.Projects.Queries;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Projects
{
    [Route("projects/projects")]
    public class ProjectController : BaseController
    {
        public ProjectController(IMediator mediator) : base(mediator)
        {
        }

        #region Get by id

        [HttpPost("get-by-id")]
        [ProducesResponseType(typeof(GetProjectResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectById([FromBody] GetProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get by id

        #region Get all

        [AllowAnonymousToken]
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllProjectsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects([FromBody] GetAllProjectsRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get all

        #region Add

        [AuthorizeToken]
        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddProjectFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProject([FromBody] GetAddProjectFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken]
        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateAddProject([FromBody] AddProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken]
        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [AuthorizeToken]
        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateProjectFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProject([FromBody] GetUpdateProjectFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken]
        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateUpdateProject([FromBody] UpdateProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken]
        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Delete

        [AuthorizeToken]
        [HttpPost("delete/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateDeleteProject([FromBody] DeleteProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken]
        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete
    }
}

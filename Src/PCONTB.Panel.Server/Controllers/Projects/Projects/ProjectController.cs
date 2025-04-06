using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands;
using PCONTB.Panel.Application.Functions.Projects.Projects.Queries;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Projects
{
    [Route("project")]
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

        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllProjectsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects([FromBody] GetAllProjectsRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get all

        #region Add

        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddProjectFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProject([FromBody] GetAddProjectFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateAddProject([FromBody] AddProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("add")]
        [ProducesResponseType(typeof(CreateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateProjectFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProject([FromBody] GetUpdateProjectFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateUpdateProject([FromBody] UpdateProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("update")]
        [ProducesResponseType(typeof(UpdateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Delete

        [HttpPost("delete/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateDeleteProject([FromBody] DeleteProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("delete")]
        [ProducesResponseType(typeof(DeleteResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete
    }
}

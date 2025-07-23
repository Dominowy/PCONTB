using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Functions.Projects.Commands;
using PCONTB.Panel.Application.Functions.Projects.Queries;
using PCONTB.Panel.Domain.Account.Users.Roles;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;
using PCONTB.Panel.Server.Filters;

namespace PCONTB.Panel.Server.Controllers.Projects
{
    [Route("api/projects/projects")]
    public class ProjectController(IMediator mediator) : BaseController(mediator)
    {
        #region Get by id

        [AllowAnonymousToken]
        [HttpPost("get-by-id")]
        [ProducesResponseType(typeof(GetProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectById([FromBody] GetProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get by id

        #region Get all

        [AllowAnonymousToken]
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllProjectsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects([FromBody] GetAllProjectsRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AllowAnonymousToken]
        [HttpPost("get-all-by-user-id")]
        [ProducesResponseType(typeof(GetAllProjectsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByUserIdProjects([FromBody] GetAllByUserIdProjectsRequest request, CancellationToken cancellation) => await Send(request, cancellation);


        #endregion Get all

        #region Add

        [AuthorizeToken(Role.User)]
        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddProjectFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProjectForm([FromBody] GetAddProjectFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.User)]
        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateAddProject([FromBody] AddProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.User)]
        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [AuthorizeToken(Role.User)]
        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateProjectFormResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProjectForm([FromBody] GetUpdateProjectFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.User)]
        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdateProject([FromBody] UpdateProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.User)]
        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Delete

        [AuthorizeToken(Role.User)]
        [HttpPost("delete/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateDeleteProject([FromBody] DeleteProjectRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.User)]
        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete

        #region Collaborator

        [AuthorizeToken(Role.User)]
        [HttpPost("get-collaborator-user")]
        [ProducesResponseType(typeof(GetProjectCollaboratorUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCollaboratorUser([FromBody] GetProjectCollaboratorUserRequest request, CancellationToken cancellation) => await Send(request, cancellation);


        #endregion Collaborator
    }
}
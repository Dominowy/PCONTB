using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Commands;
using PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Queries;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Projects.Collaborators
{
    [Route("api/projects/collaborators")]
    public class CollaboratorsController(IMediator mediator) : BaseController(mediator)
    {
        #region Get all

        [AllowAnonymousToken]
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllProjectCollaboratorsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCollaborators([FromBody] GetAllProjectCollaboratorsRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get all

        #region Add

        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddProjectCollaboratorFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCollaborator([FromBody] GetAddProjectCollaboratorFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateAddCollaborator([FromBody] AddProjectCollaboratorRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCollaborator([FromBody] AddProjectCollaboratorRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateProjectCollaboratorFormResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCollaboratorForm([FromBody] GetUpdateProjectCollaboratorFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdateCollaborator([FromBody] UpdateProjectCollaboratorRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCollaborator([FromBody] UpdateProjectCollaboratorRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Delete

        [HttpPost("delete/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateDeleteCollaborator([FromBody] DeleteProjectCollaboratorRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCollaborator([FromBody] DeleteProjectCollaboratorRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete
    }
}

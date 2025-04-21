using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Functions.Projects.Collaborators.Commands;
using PCONTB.Panel.Application.Functions.Projects.Collaborators.Queries;
using PCONTB.Panel.Infrastructure.Security.Filters;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Projects.Collaborators
{
    [Route("api/projects/collaborators")]
    public class CollaboratorsController(IMediator mediator) : BaseController(mediator)
    {
        #region Get all

        [AllowAnonymousToken]
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllCollaboratorsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCollaborators([FromBody] GetAllCollaboratorsRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get all

        #region Add

        [HttpPost("add/form")]
        [ProducesResponseType(typeof(GetAddCollaboratorFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCollaborator([FromBody] GetAddCollaboratorFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateAddCollaborator([FromBody] AddCollaboratorRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCollaborator([FromBody] AddCollaboratorRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [HttpPost("update/form")]
        [ProducesResponseType(typeof(GetUpdateCollaboratorFormResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCollaboratorForm([FromBody] GetUpdateCollaboratorFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateUpdateCollaborator([FromBody] UpdateCollaboratorRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCollaborator([FromBody] UpdateCollaboratorRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Delete

        [HttpPost("delete/validate")]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateDeleteCollaborator([FromBody] DeleteCollaboratorRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [HttpPost("delete")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCollaborator([FromBody] DeleteCollaboratorRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Delete
    }
}

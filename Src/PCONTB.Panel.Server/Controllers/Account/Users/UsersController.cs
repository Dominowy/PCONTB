using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Common.Functions.Tables;
using PCONTB.Panel.Application.Functions.Account.Users.Commands;
using PCONTB.Panel.Application.Functions.Account.Users.Queries;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Account.Users.Roles;
using PCONTB.Panel.Server.Controllers.Common;
using PCONTB.Panel.Server.Filters;

namespace PCONTB.Panel.Server.Controllers.Account.Users
{
    [Route("api/account/users")]
    public class UsersController(IMediator mediator) : BaseController(mediator)
    {
        #region Get by id

        [AllowAnonymousToken]
        [HttpPost("get-by-id")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById([FromBody] GetUserRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get by id

        #region Get all
        [AuthorizeToken(Role.Admin)]
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(GetAllUsersResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers([FromBody] GetAllUsersRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Get all

        #region Table

        [AuthorizeToken(Role.Admin)]
        [HttpPost("table/get-data")]
        [ProducesResponseType(typeof(GetAllUsersResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersTable([FromBody] PagedQueryRequest<UserTableDto> request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Table

        #region Add

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add/form")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserForm([FromBody] GetAddUserFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add/validate")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateAddUser([FromBody] AddUserRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("add")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Add

        #region Update

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("update/form")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserForm([FromBody] GetUpdateUserFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("update/validate")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdateUser([FromBody] UpdateUserRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("update")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update

        #region Update role

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update-role/form")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserRoleForm([FromBody] GetUpdateUserRoleFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update-role/validate")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdateUserRole([FromBody] UpdateUserRoleRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.Admin)]
        [HttpPost("update-role")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update role

        #region Update password

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("update-password/form")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserPasswordForm([FromBody] GetUpdatePasswordFormRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("update-password/validate")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateUpdatePassword([FromBody] UpdatePasswordRequest request, CancellationToken cancellation) => await Validate(request, cancellation);

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("update-password")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Update role

        #region Lock/Unlock

        [AuthorizeToken(Role.User, Role.Moderator, Role.Admin)]
        [HttpPost("lock")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LockUser([FromBody] LockUserRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        [AuthorizeToken(Role.Moderator, Role.Admin)]
        [HttpPost("unlock")]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommandResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnlockUser([FromBody] UnlockUserRequest request, CancellationToken cancellation) => await Send(request, cancellation);

        #endregion Lock/Unlock
    }
}
using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;

namespace PCONTB.Panel.Application.Functions.Account.Auth.Commands
{
    public class ResetUserPasswordRequest : IRequest<CommandResult>
    {
    }
}

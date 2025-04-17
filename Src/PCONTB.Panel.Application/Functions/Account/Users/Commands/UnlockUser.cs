using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;

namespace PCONTB.Panel.Application.Functions.Account.Users.Commands
{
    public class UnlockUserRequest : IRequest<CommandResult>
    {
    }
}
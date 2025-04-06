using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;

namespace PCONTB.Panel.Application.Functions.Account.Authentication.Commands
{
    public class ResetUserPasswordRequest : IRequest<UpdateResult>
    {
    }
}

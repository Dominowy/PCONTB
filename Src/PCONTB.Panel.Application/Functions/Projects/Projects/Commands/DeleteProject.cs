using MediatR;
using PCONTB.Panel.Application.Common.Models.Result;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Commands
{
    public class DeleteProjectRequest : IRequest<DeleteResult>
    {
    }
}

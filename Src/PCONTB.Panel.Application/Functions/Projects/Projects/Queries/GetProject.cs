using MediatR;
using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetProjectRequest : IRequest<GetProjectResponse>
    {
    }

    public class GetProjectResponse : BaseResponse
    {
    }
}

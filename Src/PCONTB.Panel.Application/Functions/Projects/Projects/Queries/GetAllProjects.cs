using MediatR;
using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetAllProjectsRequest : IRequest<GetAllProjectsResponse>
    {
    }

    public class GetAllProjectsResponse : BaseResponse
    {
    }
}

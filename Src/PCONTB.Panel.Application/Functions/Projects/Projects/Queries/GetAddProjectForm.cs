using MediatR;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Queries
{
    public class GetAddProjectFormRequest : IRequest<GetAddProjectFormResponse>
    {
    }

    public class GetAddProjectFormHandler : IRequestHandler<GetAddProjectFormRequest, GetAddProjectFormResponse>
    {
        public async Task<GetAddProjectFormResponse> Handle(GetAddProjectFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetAddProjectFormResponse()
            {
                FormData = new AddProjectRequest()
            });
        }
    }

    public class GetAddProjectFormResponse
    {
        public AddProjectRequest FormData { get; set; }
    }
}
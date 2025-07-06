using MediatR;
using PCONTB.Panel.Application.Functions.Projects.Projects.Projects.Commands;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Projects.Queries
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
                Form = new AddProjectRequest()
            });
        }
    }

    public class GetAddProjectFormResponse
    {
        public AddProjectRequest Form { get; set; }
    }
}
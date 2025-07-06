using MediatR;
using PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Commands;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.ProjectCollaborators.Queries
{
    public class GetAddProjectCollaboratorFormRequest : IRequest<GetAddProjectCollaboratorFormResponse>
    {

    }

    public class GetAddCollaboratorFormHandler : IRequestHandler<GetAddProjectCollaboratorFormRequest, GetAddProjectCollaboratorFormResponse>
    {
        public async Task<GetAddProjectCollaboratorFormResponse> Handle(GetAddProjectCollaboratorFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetAddProjectCollaboratorFormResponse()
            {
                Form = new AddProjectCollaboratorRequest()
            });
        }
    }

    public class GetAddProjectCollaboratorFormResponse
    {
        public AddProjectCollaboratorRequest Form { get; set; }
    }
}

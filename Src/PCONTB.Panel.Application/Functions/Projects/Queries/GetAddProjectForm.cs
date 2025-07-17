using MediatR;
using PCONTB.Panel.Application.Common.Extensions.Helpers.Enums;
using PCONTB.Panel.Application.Functions.Projects.Commands;
using PCONTB.Panel.Domain.Projects.Campaigns;

namespace PCONTB.Panel.Application.Functions.Projects.Queries
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
                Form = new AddProjectRequest(),
                ProjectCampaignContentType = EnumHelper.EnumToList<ProjectCampaignContentType>()
            });
        }
    }

    public class GetAddProjectFormResponse
    {
        public AddProjectRequest Form { get; set; }
        public List<EnumItem> ProjectCampaignContentType { get; set; } = [];

    }
}
using MediatR;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Functions.Projects.Files.Images.Commands;

namespace PCONTB.Panel.Application.Functions.Projects.Files.Images.Queries
{
    public class GetAddImageFormRequest : BaseQuery, IRequest<GetAddImageFormResponse>
    {

    }

    public class GetAddImageFormHandler : IRequestHandler<GetAddImageFormRequest, GetAddImageFormResponse>
    {
        public async Task<GetAddImageFormResponse> Handle(GetAddImageFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetAddImageFormResponse
            {
                Form = new AddImageRequest()
            });
        }
    }

    public class GetAddImageFormResponse
    {
        public AddImageRequest Form { get; set; }
    }
}

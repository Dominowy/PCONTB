using MediatR;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Functions.Projects.Images.Commands;

namespace PCONTB.Panel.Application.Functions.Projects.Images.Queries
{
    public class GetAddImageFormRequest : IRequest<GetAddImageFormResponse>
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

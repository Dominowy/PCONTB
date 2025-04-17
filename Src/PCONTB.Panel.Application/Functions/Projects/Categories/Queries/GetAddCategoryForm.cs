using MediatR;
using PCONTB.Panel.Application.Functions.Projects.Categories.Commands;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class GetAddCategoryFormRequest : IRequest<GetAddCategoryFormResponse>
    {
    }

    public class GetAddCategoryFormHandler : IRequestHandler<GetAddCategoryFormRequest, GetAddCategoryFormResponse>
    {
        public async Task<GetAddCategoryFormResponse> Handle(GetAddCategoryFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetAddCategoryFormResponse
            {
                Form = new AddCategoryRequest()
            });
        }
    }

    public class GetAddCategoryFormResponse
    {
        public AddCategoryRequest Form { get; set; }
    }
}
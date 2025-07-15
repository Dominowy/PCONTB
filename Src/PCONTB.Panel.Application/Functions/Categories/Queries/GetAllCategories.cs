using MediatR;
using PCONTB.Panel.Application.Models.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
{
    public class GetAllCategoriesRequest : IRequest<GetAllCategoriesResponse>
    {
    }

    public class GetAllCategoriesHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CategoryRepository.GetAll(cancellationToken);

            return new GetAllCategoriesResponse
            {
                Categories = [.. entity.Select(CategoryDto.Map)]
            };
        }
    }

    public class GetAllCategoriesResponse
    {
        public List<CategoryDto> Categories { get; set; }
    }
}
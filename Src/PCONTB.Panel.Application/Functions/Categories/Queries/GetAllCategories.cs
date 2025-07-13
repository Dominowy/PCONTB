using MediatR;
using PCONTB.Panel.Application.Models.Projects.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
{
    public class GetAllCategoriesRequest : IRequest<GetAllCategoriesResponse>
    {
    }

    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAll(cancellationToken);

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
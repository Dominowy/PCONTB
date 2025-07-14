using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Models.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
{
    public class GetCategoryRequest : BaseQuery, IRequest<GetCategoryResponse>
    {

    }

    public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, GetCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Categories.Category.NotFound.Message);

            return new GetCategoryResponse
            {
                Category = CategoryDto.Map(entity)
            };
        }
    }

    public class GetCategoryResponse
    {
        public CategoryDto Category { get; internal set; }
    }
}
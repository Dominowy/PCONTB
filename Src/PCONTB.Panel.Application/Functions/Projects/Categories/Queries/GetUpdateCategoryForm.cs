using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Functions.Projects.Categories.Commands;
using PCONTB.Panel.Application.Models.Projects.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class GetUpdateCategoryFormRequest : BaseQuery, IRequest<GetUpdateCategoryFormResponse>
    {

    }

    public class GetUpdateCategoryFormHandler : IRequestHandler<GetUpdateCategoryFormRequest, GetUpdateCategoryFormResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUpdateCategoryFormHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUpdateCategoryFormResponse> Handle(GetUpdateCategoryFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken);
            
            if (entity is null) throw new NotFoundException(ErrorCodes.Category.NotFound.Message);

            return new GetUpdateCategoryFormResponse
            {
                Form = new UpdateCategoryRequest
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Subcategories = [.. entity.Subcategories.Select(SubcategoryDto.Map)]
                }
            };
        }
    }

    public class GetUpdateCategoryFormResponse
    {
        public UpdateCategoryRequest Form { get; set; }
    }
}

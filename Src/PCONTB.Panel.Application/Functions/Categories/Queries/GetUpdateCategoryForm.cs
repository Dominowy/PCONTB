using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Functions.Categories.Commands;
using PCONTB.Panel.Application.Models.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
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
            
            if (entity is null) throw new NotFoundException(ErrorCodes.Categories.Category.NotFound.Message);

            return new GetUpdateCategoryFormResponse
            {
                Form = new UpdateCategoryRequest
                {
                    Id = entity.Id,
                    Name = entity.Name,
                }
            };
        }
    }

    public class GetUpdateCategoryFormResponse
    {
        public UpdateCategoryRequest Form { get; set; }
    }
}

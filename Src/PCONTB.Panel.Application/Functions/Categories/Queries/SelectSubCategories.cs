using MediatR;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Select;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
{
    public class SelectSubCategoriesRequest : BaseQuery, IRequest<SelectResponse>
    {
    }

    public class SelectSubCategoriesHandler : IRequestHandler<SelectSubCategoriesRequest, SelectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SelectSubCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SelectResponse> Handle(SelectSubCategoriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new SelectResponse
                {
                    Data = [],
                };
            }

            return new SelectResponse
            {
                Data = [.. entity.Subcategories.Select(m => new SelectData(m.Id, m.Name))],
            };
        }
    }
}

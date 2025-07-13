using MediatR;
using PCONTB.Panel.Application.Common.Models.Select;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
{
    public class SelectCategoriesRequest : IRequest<SelectResponse>
    {
    }

    public class SelectCategoriesHandler : IRequestHandler<SelectCategoriesRequest, SelectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SelectCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SelectResponse> Handle(SelectCategoriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetAll(cancellationToken);

            return new SelectResponse
            {
                Data = [.. entity.Select(m => new SelectData(m.Id, m.Name))],
            };
        }
    }
}

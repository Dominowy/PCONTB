using MediatR;
using PCONTB.Panel.Application.Common.Functions.Selects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Queries
{
    public class SelectCategoriesRequest : IRequest<SelectResponse>
    {
        public Guid IncludedId { get; set; }
    }

    public class SelectCategoriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<SelectCategoriesRequest, SelectResponse>
    {
        public async Task<SelectResponse> Handle(SelectCategoriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CategoryRepository.GetAll(m => m.Enabled || m.Id == request.IncludedId, cancellationToken);

            return new SelectResponse
            {
                Data = [.. entity.Select(m => new SelectData(m.Id, m.Name))],
            };
        }
    }
}

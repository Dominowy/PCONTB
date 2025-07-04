using MediatR;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Select;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class SelectCountriesRequest : BaseQuery, IRequest<SelectResponse>
    {
    }

    public class SelectCountriesHandler : IRequestHandler<SelectCountriesRequest, SelectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SelectCountriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SelectResponse> Handle(SelectCountriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CountryRepository.GetAll(cancellationToken);

            return new SelectResponse
            {
                Data = [.. entity.Select(m => new SelectData(m.Id, m.Name))],
            };
        }
    }
}

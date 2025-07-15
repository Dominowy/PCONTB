using MediatR;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Common.Functions.Selects;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class SelectCountriesRequest : BaseQuery, IRequest<SelectResponse>
    {
        public Guid IncludedId { get; set; }
    }

    public class SelectCountriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<SelectCountriesRequest, SelectResponse>
    {
        public async Task<SelectResponse> Handle(SelectCountriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CountryRepository.GetAll(m => m.Enabled || m.Id == request.IncludedId, cancellationToken);

            return new SelectResponse
            {
                Data = [.. entity.Select(m => new SelectData(m.Id, m.Name))],
            };
        }
    }
}

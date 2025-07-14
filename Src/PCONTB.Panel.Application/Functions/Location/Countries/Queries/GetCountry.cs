using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Models.Locations.Countries;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class GetCountryRequest : BaseQuery, IRequest<GetCountryResponse>
    {

    }

    public class GetCountryHandler : IRequestHandler<GetCountryRequest, GetCountryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCountryResponse> Handle(GetCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CountryRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Countries.Country.NotFound.Message);

            return new GetCountryResponse
            {
                Country = CountryDto.Map(entity)
            };
        }
    }

    public class GetCountryResponse
    {
        public CountryDto Country { get; set; }
    }
}
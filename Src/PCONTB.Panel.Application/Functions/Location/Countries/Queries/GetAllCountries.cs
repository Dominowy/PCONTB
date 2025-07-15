using MediatR;
using PCONTB.Panel.Application.Models.Locations.Countries;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class GetAllCountriesRequest : IRequest<GetAllCountriesResponse>
    {
    }

    public class GetAllCountriesHandler(IUnitOfWork unitOfWork) 
        : IRequestHandler<GetAllCountriesRequest, GetAllCountriesResponse>
    {
        public async Task<GetAllCountriesResponse> Handle(GetAllCountriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CountryRepository.GetAll(cancellationToken);

            return new GetAllCountriesResponse
            {
                Countries = [.. entity.Select(CountryDto.Map)]
            };
        }
    }

    public class GetAllCountriesResponse
    {
        public List<CountryDto> Countries { get; set; }
    }
}

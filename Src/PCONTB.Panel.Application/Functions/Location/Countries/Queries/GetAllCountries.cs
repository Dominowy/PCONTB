using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Dto.Locations.Countries;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class GetAllCountriesRequest : IRequest<GetAllCountriesResponse>
    {
    }

    public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesRequest, GetAllCountriesResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllCountriesHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllCountriesResponse> Handle(GetAllCountriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Country>().ToListAsync(cancellationToken);


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

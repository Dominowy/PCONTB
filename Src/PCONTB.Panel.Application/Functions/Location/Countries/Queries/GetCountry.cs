using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Dto.Locations.Countries;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class GetCountryRequest : IRequest<GetCountryResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetCountryHandler : IRequestHandler<GetCountryRequest, GetCountryResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCountryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetCountryResponse> Handle(GetCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Country>().FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Country.NotFound.Message);

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
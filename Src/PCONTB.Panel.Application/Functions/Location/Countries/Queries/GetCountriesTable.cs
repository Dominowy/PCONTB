using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Locations.Countries;
using PCONTB.Panel.Application.Table;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class CountryPagedQueryHandler : PagedQueryHandler<Country, CountryTableDto>
    {
        private readonly IApplicationDbContext _dbContext;

        public CountryPagedQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<PagedResultDto<CountryTableDto>> Handle(PagedQueryRequest<CountryTableDto> request, CancellationToken cancellationToken)
        {
            var query = GetQuery();

            SetQuery(query);

            return await base.Handle(request, cancellationToken);
        }

        protected override IQueryable<Country> GetQuery()
        {
            return _dbContext.Set<Country>();
        }

        protected override string[] GetGlobalSearchProperties()
        {
            return new[] { "Name" };
        }

        protected override CountryTableDto MapEntityToDto(Country country)
        {
            return new CountryTableDto
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}

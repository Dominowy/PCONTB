using PCONTB.Panel.Application.Common.Functions.Tables;
using PCONTB.Panel.Application.Models.Locations.Countries;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class CountryPagedQueryHandler(IUnitOfWork unitOfWork) : PagedQueryHandler<Country, CountryTableDto>
    {
        public override async Task<PagedResultDto<CountryTableDto>> Handle(PagedQueryRequest<CountryTableDto> request, CancellationToken cancellationToken)
        {
            var query = GetQuery();

            SetQuery(query);

            return await base.Handle(request, cancellationToken);
        }

        protected override IQueryable<Country> GetQuery()
        {
            return unitOfWork.CountryRepository.GetQuery();
        }

        protected override string[] GetGlobalSearchProperties()
        {
            return new[] { "Name", "Enabled" };
        }

        protected override CountryTableDto MapEntityToDto(Country country)
        {
            return new CountryTableDto
            {
                Id = country.Id,
                Name = country.Name,
                Enabled = country.Enabled
            };
        }
    }
}

using PCONTB.Panel.Application.Common.Functions.Tables;
using PCONTB.Panel.Application.Models.Locations.Countries;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class CountryPagedQueryHandler : PagedQueryHandler<Country, CountryTableDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryPagedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<PagedResultDto<CountryTableDto>> Handle(PagedQueryRequest<CountryTableDto> request, CancellationToken cancellationToken)
        {
            var query = GetQuery();

            SetQuery(query);

            return await base.Handle(request, cancellationToken);
        }

        protected override IQueryable<Country> GetQuery()
        {
            return _unitOfWork.CountryRepository.GetQuery();
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

using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

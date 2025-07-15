using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Country?> GetBy(Expression<Func<Country, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .Include(c => c.Projects)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

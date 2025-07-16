using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class CountryRepository(ApplicationDbContext context) 
        : Repository<Country>(context), ICountryRepository
    {
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

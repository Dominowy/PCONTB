using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<Category?> GetBy(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

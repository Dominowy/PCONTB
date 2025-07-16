using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) 
        : Repository<Category>(context), ICategoryRepository
    {
        public override async Task<Category?> GetBy(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .Include(c => c.Projects)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

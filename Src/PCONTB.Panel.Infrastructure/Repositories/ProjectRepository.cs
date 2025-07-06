using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Projects.Projects;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Project>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(p => p.User)
                .Include(p => p.Country)
                .Include(p => p.Collaborators).ThenInclude(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Image)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<Project>> GetAll(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(p => p.User)
                .Include(p => p.Country)
                .Include(p => p.Collaborators).ThenInclude(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Image)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<Project?> GetBy(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(p => p.User)
                .Include(p => p.Collaborators).ThenInclude(p => p.User)
                .Include(p => p.Country)
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Image)
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Project?> GetByTracking(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<Project>()
                .Include(p => p.User)
                .Include(p => p.Collaborators).ThenInclude(p => p.User)
                .Include(p => p.Country)
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Image)
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

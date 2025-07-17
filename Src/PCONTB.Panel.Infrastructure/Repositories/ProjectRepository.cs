using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class ProjectRepository(ApplicationDbContext context) 
        : Repository<Project>(context), IProjectRepository
    {
        public override async Task<IEnumerable<Project>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(p => p.User)
                .Include(p => p.Country)
                .Include(p => p.Collaborators).ThenInclude(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Image)
                .Include(p => p.Video)
                .Include(p => p.Campaing).ThenInclude(p => p.CampaignContents)
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
                .Include(p => p.Image)
                .Include(p => p.Video)
                .Include(p => p.Campaing).ThenInclude(p => p.CampaignContents)
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
                .Include(p => p.Image)
                .Include(p => p.Video)
                .Include(p => p.Campaing).ThenInclude(p => p.CampaignContents)
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Project?> GetByTracking(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Include(p => p.User)
                .Include(p => p.Collaborators).ThenInclude(p => p.User)
                .Include(p => p.Country)
                .Include(p => p.Category)
                .Include(p => p.Image)
                .Include(p => p.Video)
                .Include(p => p.Campaing).ThenInclude(p => p.CampaignContents)
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

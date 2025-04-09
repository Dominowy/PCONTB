using Microsoft.EntityFrameworkCore;

namespace PCONTB.Panel.Application.Contracts.Infrastructure.DbContext
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace PCONTB.Panel.Application.Contracts.DbContext
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

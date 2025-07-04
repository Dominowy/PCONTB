using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        internal ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> GetQuery()
        {
            return dbSet.AsNoTracking();
        }

        public virtual async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task Add(T entity, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task Update(T entityToUpdate, CancellationToken cancellationToken)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public virtual async Task Delete(T entityToDelete, CancellationToken cancellationToken)
        {
            dbSet.Remove(entityToDelete);
            await Task.CompletedTask;
        }

        public virtual async Task<bool> Exist(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
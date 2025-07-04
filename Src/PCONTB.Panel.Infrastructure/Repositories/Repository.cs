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

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsyncByPredicate(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            var result = await dbSet.Where(predicate).ToListAsync(cancellationToken);

            return await Task.FromResult(result);
        }

        public virtual async Task<T> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(id, cancellationToken);
        }

        public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task UpdateAsync(T entityToUpdate, CancellationToken cancellationToken)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task DeleteAsync(Guid? id, CancellationToken cancellationToken)
        {
            var entityToDelete = await dbSet.FindAsync(id, cancellationToken);
            dbSet.Remove(entityToDelete);
        }

        public virtual async Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
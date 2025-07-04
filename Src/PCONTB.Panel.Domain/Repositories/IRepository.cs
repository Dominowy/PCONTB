using System.Linq.Expressions;

namespace PCONTB.Panel.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsyncByPredicate(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        Task InsertAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid? id, CancellationToken cancellationToken);
        Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}

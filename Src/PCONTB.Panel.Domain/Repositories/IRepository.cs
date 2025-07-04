using System.Linq.Expressions;

namespace PCONTB.Panel.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T> GetBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task Add(T entity, CancellationToken cancellationToken);
        Task Update(T entity, CancellationToken cancellationToken);
        Task Delete(T entity, CancellationToken cancellationToken);
        Task<bool> Exist(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}

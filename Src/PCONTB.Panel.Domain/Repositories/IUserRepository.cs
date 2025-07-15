using PCONTB.Panel.Domain.Account.Users;
using System.Linq.Expressions;

namespace PCONTB.Panel.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByTracking(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
    }
}
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using System.Linq.Expressions;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<User> GetByPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate).Include(m => m.UserRoles)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

using PCONTB.Panel.Domain.Projects;
using System.Linq.Expressions;

namespace PCONTB.Panel.Domain.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetByTracking(Expression<Func<Project, bool>> predicate, CancellationToken cancellationToken);
    }
}
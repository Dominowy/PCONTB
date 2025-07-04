using PCONTB.Panel.Domain.Projects.Projects;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

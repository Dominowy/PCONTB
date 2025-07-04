using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private ISessionRepository _session;
        private IUserRepository _user;
        private ICountryRepository _country;
        private ICategoryRepository _category;
        private IProjectRepository _project;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ISessionRepository SessionRepository => _session ??= new SessionRepository(_dbContext);

        public IUserRepository UserRepository => _user ??= new UserRepository(_dbContext);

        public ICountryRepository CountryRepository => _country ??= new CountryRepository(_dbContext);

        public ICategoryRepository CategoryRepository => _category ??= new CategoryRepository(_dbContext);

        public IProjectRepository ProjectRepository => _project ??= new ProjectRepository(_dbContext);

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
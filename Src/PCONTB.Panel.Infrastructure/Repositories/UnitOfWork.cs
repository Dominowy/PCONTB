using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        private ISessionRepository _session;
        private IUserRepository _user;
        private ICountryRepository _country;
        private ICategoryRepository _category;
        private IProjectRepository _project;

        public ISessionRepository SessionRepository => _session ??= new SessionRepository(dbContext);

        public IUserRepository UserRepository => _user ??= new UserRepository(dbContext);

        public ICountryRepository CountryRepository => _country ??= new CountryRepository(dbContext);

        public ICategoryRepository CategoryRepository => _category ??= new CategoryRepository(dbContext);

        public IProjectRepository ProjectRepository => _project ??= new ProjectRepository(dbContext);


        public async Task Save(CancellationToken cancellationToken)
        {
            var entries = dbContext.ChangeTracker.Entries();
            foreach (var e in entries)
                Console.WriteLine($"{e.Entity.GetType().Name} = {e.State}");

            await dbContext.SaveChangesAsync(cancellationToken);
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
                dbContext.Dispose();
            }
        }
    }
}
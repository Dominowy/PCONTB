namespace PCONTB.Panel.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ISessionRepository SessionRepository { get; }
        public IUserRepository UserRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IMessageRepository MessageRepository { get; }

        Task Save(CancellationToken cancellationToken);
    }
}

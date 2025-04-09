using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account.Sessions
{
    public class Session : Entity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public DateTimeOffset Started { get; private set; }
        public DateTimeOffset Ended { get; private set; }
        public DateTimeOffset LastActivity { get; private set; }

        public bool IsActive { get; private set; }

        public string? Device { get; private set; }
        public string? IpAddress { get; private set; }
        public string? Location { get; private set; }
        public string? OperatingSystem { get; private set; }
        public string? Browser { get; private set; }

        protected Session() : base() 
        {
            
        }

        public Session(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
            Started = DateTimeOffset.UtcNow;
            Ended = DateTimeOffset.UtcNow.AddDays(7);
            LastActivity = DateTimeOffset.UtcNow;
            IsActive = true;
        }

        public void RefreshLastActivity()
        {
            LastActivity = DateTimeOffset.UtcNow;
        }

        public void EndSession()
        {
            IsActive = false;
            Ended = DateTimeOffset.UtcNow;
            LastActivity = DateTimeOffset.UtcNow;
        }
    }
}

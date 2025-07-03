using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account.Sessions
{
    public class Session : BaseAggregateEnabled
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public DateTimeOffset Started { get; private set; }
        public DateTimeOffset Ended { get; private set; }
        public DateTimeOffset LastActivity { get; private set; }

        protected Session() : base() 
        {
            
        }

        public Session(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
            Started = DateTimeOffset.UtcNow;
            Ended = DateTimeOffset.UtcNow.AddDays(7);
            LastActivity = DateTimeOffset.UtcNow;
            SetEnabled(true);
        }

        public void RefreshLastActivity()
        {
            LastActivity = DateTimeOffset.UtcNow;
        }

        public void EndSession()
        {
            SetEnabled(false);
            Ended = DateTimeOffset.UtcNow;
            LastActivity = DateTimeOffset.UtcNow;
        }
    }
}

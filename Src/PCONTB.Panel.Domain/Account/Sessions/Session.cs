using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account.Sessions
{
    public class Session : Entity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTimeOffset Started { get; set; }
        public DateTimeOffset Ended { get; set; }
        public DateTimeOffset LastActivity { get; set; }

        public bool IsActive { get; set; }

        public string Device { get; set; }
        public string IpAddress { get; set; }
        public string? Location { get; set; }
        public string? OperatingSystem { get; set; }
        public string? Browser { get; set; }
    }
}

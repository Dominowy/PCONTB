using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Domain.Account.Users.Wallets
{
    public class UserWallet : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string Address { get; set; }
        public WalletType Type { get; set; }
    }
}

using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCONTB.Panel.Domain.Account.Users.Favorites
{
    public class UserProjectFavorite : BaseEntity
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
    }
}

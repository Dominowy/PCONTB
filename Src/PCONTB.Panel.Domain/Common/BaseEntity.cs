using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCONTB.Panel.Domain.Common
{
    public abstract class BaseEntity : Entity
    {
        protected BaseEntity() : base()
        {

        }

        protected BaseEntity(Guid id) : base(id)
        {

        }
    }
}

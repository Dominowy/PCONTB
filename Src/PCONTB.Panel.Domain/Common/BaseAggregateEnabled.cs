using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCONTB.Panel.Domain.Common
{
    public abstract class BaseAggregateEnabled : Aggregate, IHasEnabled
    {
        public bool Enabled { get; private set; } = true;

        protected BaseAggregateEnabled() : base()
        {

        }

        protected BaseAggregateEnabled(Guid id) : base(id)
        {

        }

        public void SetEnabled(bool enabled)
        {
            var anyChange = Enabled != enabled;
            if (!anyChange) return;
            Enabled = enabled;
        }
    }
}

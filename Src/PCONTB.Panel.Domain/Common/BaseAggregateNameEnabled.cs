using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCONTB.Panel.Domain.Common
{
    public abstract class BaseAggregateNameEnabled : Aggregate, IHasEnabled, IHasName
    {
        public bool Enabled { get; private set; } = true;

        public string Name { get; private set; }

        protected BaseAggregateNameEnabled() : base()
        {

        }

        protected BaseAggregateNameEnabled(Guid id) : base(id)
        {

        }

        public void SetEnabled(bool enabled)
        {
            var anyChange = Enabled != enabled;
            if (!anyChange) return;
            Enabled = enabled;
        }

        public void SetName(string name)
        {
            var anyChange = Name != name;
            if (!anyChange) return;
            Name = name;
        }
    }
}

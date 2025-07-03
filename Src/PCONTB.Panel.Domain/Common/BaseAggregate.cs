namespace PCONTB.Panel.Domain.Common
{
    public abstract class BaseAggregate : Aggregate
    {
        protected BaseAggregate() : base()
        {

        }

        protected BaseAggregate(Guid id) : base(id)
        {

        }
    }
}

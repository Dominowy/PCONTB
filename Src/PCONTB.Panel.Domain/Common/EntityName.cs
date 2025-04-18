namespace PCONTB.Panel.Domain.Common
{
    public class EntityName : Entity
    {
        public string Name { get; set; }

        protected EntityName(Guid id) : base(id) 
        {

        }
    }
}

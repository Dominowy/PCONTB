namespace PCONTB.Panel.Application.Common.Functions.Selects
{
    public class SelectData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public SelectData(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

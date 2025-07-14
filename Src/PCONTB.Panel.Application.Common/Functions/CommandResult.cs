namespace PCONTB.Panel.Application.Common
{
    public class CommandResult
    {
        public Guid Id { get; set; }
        public CommandResult(Guid id)
        {
            Id = id;
        }
    }
}

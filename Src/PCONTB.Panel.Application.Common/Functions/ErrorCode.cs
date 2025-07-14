namespace PCONTB.Panel.Application.Common
{
    public record struct ErrorCode(string Code, string Message) : IErrorCode
    {
    }

    public interface IErrorCode
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}

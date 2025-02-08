using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Common.Models.Result
{
    public class SessionResult : BaseResponse
    {
        public Guid UserId { get; set; }
        public SessionResult() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.OK;
        }

        public SessionResult(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {
        }
    }
}

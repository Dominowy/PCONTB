using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Common.Models.Result
{
    public class CreateResult : BaseResponse
    {
        public CreateResult() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.Created;
        }

        public CreateResult(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {
        }
    }
}

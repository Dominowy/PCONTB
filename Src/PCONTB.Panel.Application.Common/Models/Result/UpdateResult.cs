using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Common.Models.Result
{
    public class UpdateResult : BaseResponse
    {
        public UpdateResult() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.OK;
        }

        public UpdateResult(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {
        }
    }
}

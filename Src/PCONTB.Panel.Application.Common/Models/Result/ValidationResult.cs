using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Common.Models.Result
{
    public class ValidationResult : BaseResponse
    {

        public ValidationResult(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {

        }
    }
}

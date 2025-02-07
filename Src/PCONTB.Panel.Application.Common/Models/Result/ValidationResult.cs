using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Response;

namespace PCONTB.Panel.Application.Common.Models.Result
{
    public class ValidationResult : BaseResponse
    {

        public IEnumerable<ValidationError> Errors { get; set; }

        public ValidationResult() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.OK;
        }

        public ValidationResult(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {

        }
    }
}

namespace PCONTB.Panel.Application.Common.Models.Response
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public ResponseStatus StatusCode { get; set; }

        protected BaseResponse()
        {

        }

        protected BaseResponse(bool success, ResponseStatus statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }
    }
}

using PCONTB.Panel.Application.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCONTB.Panel.Application.Common.Models.Result
{
    public class DeleteResult : BaseResponse
    {
        public DeleteResult() : base()
        {
            Success = true;
            StatusCode = ResponseStatus.NoContent;
        }

        public DeleteResult(bool success, ResponseStatus statusCode) : base(success, statusCode)
        {
        }
    }
}

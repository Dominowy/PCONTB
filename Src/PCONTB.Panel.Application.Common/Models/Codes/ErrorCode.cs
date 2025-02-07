using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCONTB.Panel.Application.Common.Models.Codes
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

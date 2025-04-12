using PCONTB.Panel.Application.Common.Models.Codes;

namespace PCONTB.Panel.Application.Common.Models.Result
{
    public class ValidationResult
    {
        public IEnumerable<ValidationError> Errors { get; set; }
    }
}

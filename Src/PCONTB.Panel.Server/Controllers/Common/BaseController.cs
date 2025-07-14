using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Common.Functions;

namespace PCONTB.Panel.Server.Controllers.Common
{
    public abstract class BaseController : ControllerBase
    {
        public IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        {
            var validator = HttpContext.RequestServices.GetService<IValidator<TRequest>>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ValidationResult
                    {
                        Errors = validationResult.Errors.Select(e => new ValidationError { PropertyName = e.PropertyName, ErrorCode = e.ErrorCode, Message = e.ErrorMessage })
                    });
                }
            }

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        protected async Task<IActionResult> Validate<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : class
        {
            var validator = HttpContext.RequestServices.GetService<IValidator<TRequest>>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ValidationResult
                    {
                        Errors = validationResult.Errors.Select(e => new ValidationError { PropertyName = e.PropertyName, ErrorCode = e.ErrorCode, Message = e.ErrorMessage })
                    });
                }
            }

            return Ok(new ValidationResult());
        }
    }


}

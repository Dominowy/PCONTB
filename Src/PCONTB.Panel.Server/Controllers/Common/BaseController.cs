using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PCONTB.Panel.Server.Controllers.Common
{
    public abstract class BaseController : ControllerBase
    {
        public IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Send<TResponse>(IRequest<TResponse> query, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(query, cancellationToken);

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
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Validation failed",
                        Errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                    });
                }
            }

            return Ok(request);
        }
    }
}

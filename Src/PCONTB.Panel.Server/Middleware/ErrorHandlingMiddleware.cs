using PCONTB.Panel.Application.Common.Exceptions;

namespace PCONTB.Panel.Server.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BadRequestException badHttpRequestException)
            {
                _logger.LogError(badHttpRequestException, badHttpRequestException.Message);

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badHttpRequestException.Message);
            }
            catch (UnauthorizedException anauthorizedException)
            {
                _logger.LogError(anauthorizedException, anauthorizedException.Message);

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(anauthorizedException.Message);
            }
            catch (ForbiddenException forbiddenException)
            {
                _logger.LogError(forbiddenException, forbiddenException.Message);

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbiddenException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
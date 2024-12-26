using AspNetCore.Proxy;
using Microsoft.AspNetCore.Mvc;

namespace PCONTB.UI.Controllers
{
    [Route("/api")]
    public class ProxyController : Controller
    {
        private readonly ServerSettings _settings;

        public ProxyController(ServerSettings settings)
        {
            _settings = settings;
        }

        [Route("{prefix}/{**url}")]
        public Task Proxy(string prefix, string url)
        {
            var queryString = Request.QueryString.Value;

            if (!_settings.ProxyEndpoints.TryGetValue(prefix, out var baseUrl))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
                return Task.CompletedTask;
            }

            baseUrl = baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/";

            return this.HttpProxyAsync(baseUrl + url + queryString);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{**rest}")]
        [HttpPut("{**rest}")]
        [HttpPost("{**rest}")]
        [HttpDelete("{**rest}")]
        [HttpHead("{**rest}")]
        [HttpOptions("{**rest}")]
        [HttpPatch("{**rest}")]
        public IActionResult Api()
        {
            return NotFound("");
        }
    }
}

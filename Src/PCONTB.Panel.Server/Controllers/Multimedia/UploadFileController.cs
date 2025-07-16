using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Multimedia
{
    [Route("api/multimedia")]
    public class UploadFileController(IMediator mediator, ISessionAccesor sessionAccesor) : BaseController(mediator)
    {
        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile()
        {
            var userId = sessionAccesor.Session.User.Id;

            var file = Request.Form.Files.FirstOrDefault();
            if (file == null) return BadRequest();

            var userTempFolder = Path.Combine(Path.GetTempPath(), userId.ToString());
            Directory.CreateDirectory(userTempFolder);

            var tempFileName = Path.GetFileName(Path.GetTempFileName());
            var tempPath = Path.Combine(userTempFolder, tempFileName);

            using (var stream = file.OpenReadStream())
            {
                using (var fileStream = System.IO.File.OpenWrite(tempPath))
                {
                    await stream.CopyToAsync(fileStream);
                    fileStream.Seek(0, SeekOrigin.Begin);
                }
            }

            return Ok(tempPath);
        }
    }
}

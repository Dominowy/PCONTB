using MediatR;
using Microsoft.AspNetCore.Mvc;
using PCONTB.Panel.Server.Controllers.Common;

namespace PCONTB.Panel.Server.Controllers.Multimedia
{
    [Route("api/multimedia/file")]
    public class FileController : BaseController
    {
        public FileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null) return BadRequest();

            var tempFileName = Path.GetFileName(Path.GetTempFileName());
            var tempPath = Path.Combine(Path.GetTempPath(), tempFileName);

            using (var stream = file.OpenReadStream())
            {
                using (var fileStream = System.IO.File.OpenWrite(tempPath))
                {
                    await stream.CopyToAsync(fileStream);
                    fileStream.Seek(0, SeekOrigin.Begin);
                }
            }

            return Json(tempPath);
        }
    }
}

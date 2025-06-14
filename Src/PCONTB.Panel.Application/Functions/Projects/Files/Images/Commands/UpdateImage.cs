using MediatR;
using Microsoft.AspNetCore.Http;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Application.Functions.Projects.Files.Images.Commands
{
    public class UpdateImageRequest : BaseCommand, IRequest<CommandResult>
    {
        public IFormFile Image { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class UpdateImageHandler : IRequestHandler<UpdateImageRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateImageHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CommandResult> Handle(UpdateImageRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<ProjectImage>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Image.NotFound.Message);

            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream, cancellationToken);

            entity.SetFileName(request.Image.FileName);
            entity.SetImageData(memoryStream.ToArray());
            entity.SetContentType(request.Image.ContentType);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

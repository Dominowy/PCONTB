using MediatR;
using Microsoft.AspNetCore.Http;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Images;

namespace PCONTB.Panel.Application.Functions.Projects.Images.Commands
{
    public class UpdateImageRequest : BaseCommand, IRequest<CommandResult>
    {
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public int DisplayOrder { get; set; }
        public Guid ProjectId { get; set; }

        public IFormFile Image { get; set; }
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
            var entity = await _dbContext.Set<Image>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Image.NotFound.Message);

            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream);

            entity.SetDispalyOrder(request.DisplayOrder);

            if (request.Image != null)
            {
                entity.SetImageName(request.Image.Name);
                entity.SetImageData(memoryStream.ToArray());
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

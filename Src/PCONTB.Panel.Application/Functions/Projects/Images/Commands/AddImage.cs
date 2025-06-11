using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Images;

namespace PCONTB.Panel.Application.Functions.Projects.Images.Commands
{
    public class AddImageRequest : BaseCommand, IRequest<CommandResult>
    {
        public List<IFormFile> Images { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class AddImageHandler : IRequestHandler<AddImageRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddImageHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CommandResult> Handle(AddImageRequest request, CancellationToken cancellationToken)
        {
            if (request.Images == null || !request.Images.Any())
                throw new BadRequestException(ErrorCodes.Image.ImageNotSelect.Message);

            var existingImages = await _dbContext.Set<Image>()
                .Where(i => i.ProjectId == request.ProjectId)
                .ToListAsync(cancellationToken);

            var lastDisplayOrder = existingImages.Any() ? existingImages.Max(m => m.DisplayOrder): 0;
            var images = new List<Image>();

            foreach (var image in request.Images)
            {
                if (image.Length == 0)
                    throw new BadRequestException($"{image.Name} " + ErrorCodes.Image.ImageNotSelect.Message);

                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream, cancellationToken);

                var entity = new Image(request.ProjectId);

                entity.SetDispalyOrder(++lastDisplayOrder);
                entity.SetImageName(image.FileName);
                entity.SetImageData(memoryStream.ToArray());

                images.Add(entity);
            }

            await _dbContext.Set<Image>().AddRangeAsync(images, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(request.ProjectId);
        }
    }
}

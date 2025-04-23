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
    public class AddImageRequest : BaseCommand, IRequest<CommandResult>
    {
        public IFormFile Image { get; set; }
        public int DisplayOrder { get; set; }
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
            if (request.Image == null || request.Image.Length == 0)
                throw new BadRequestException(ErrorCodes.Image.ImageNotSelect.Message);

            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream);

            var entity = new Image(request.ProjectId);

            entity.SetDispalyOrder(request.DisplayOrder);
            entity.SetImageName(request.Image.Name);
            entity.SetImageData(memoryStream.ToArray());

            await _dbContext.Set<Image>().AddAsync(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

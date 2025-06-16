using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Files;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Application.Functions.Projects.Files.Images.Commands
{
    public class UpdateImageRequest : BaseCommand, IRequest<CommandResult>
    {
        public FormFile Image { get; set; }
        public byte[] Data { get; set; }
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
            if (request.Image.Path == null) throw new NotFoundException(ErrorCodes.Image.PathNotExist.Message);

            var entity = await _dbContext.Set<ProjectImage>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Image.NotFound.Message);

            var data = await File.ReadAllBytesAsync(request.Image.Path, cancellationToken);

            entity.SetFileName(request.Image.FileName);
            entity.SetImageData(data);
            entity.SetContentType(request.Image.ContentType);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateImageRequesttValidator : AbstractValidator<UpdateImageRequest>
    {
        private static readonly string[] AllowedContentTypes =
        [
            "image/jpeg",
            "image/png",
            "image/webp",
            "image/gif"
        ];

        private const long MaxFileSizeInBytes = 5 * 1024 * 1024;

        public UpdateImageRequesttValidator()
        {
            RuleFor(x => x.Image)
                .NotNull().WithMessage("You must upload an image.");

            RuleFor(x => x.Image).ChildRules(images =>
            {
                //images.RuleFor(f => f.Length)
                //    .LessThanOrEqualTo(MaxFileSizeInBytes)
                //    .WithMessage("Each image must be smaller than 5 MB.");

                images.RuleFor(f => f.ContentType)
                    .Must(ct => AllowedContentTypes.Contains(ct))
                    .WithMessage("Only JPG, PNG, WEBP or GIF files are allowed.");
            });
        }
    }
}

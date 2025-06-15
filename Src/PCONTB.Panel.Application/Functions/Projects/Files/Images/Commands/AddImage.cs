using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Files;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Application.Functions.Projects.Files.Images.Commands
{
    public class AddImageRequest : BaseCommand, IRequest<CommandResult>
    {
        public FormFile Image { get; set; }
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
            if (request.Image == null)
                throw new BadRequestException(ErrorCodes.Image.ImageNotSelect.Message);

            var data = await File.ReadAllBytesAsync(request.Image.Path, cancellationToken);

            var entity = new ProjectImage(request.Image.FileName, request.Image.ContentType, data);

            await _dbContext.Set<ProjectImage>().AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddImageRequestValidator : AbstractValidator<AddImageRequest>
    {
        private static readonly string[] AllowedContentTypes =
        [
            "image/jpeg",
            "image/png",
            "image/webp",
            "image/gif"
        ];

        private const long MaxFileSizeInBytes = 5 * 1024 * 1024;

        public AddImageRequestValidator()
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
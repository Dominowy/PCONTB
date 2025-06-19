using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Files;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Functions.Projects.Files.Images.Commands;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Application.Functions.Projects.Files.Images.Queries
{
    public class GetUpdateImageFormRequest : BaseQuery, IRequest<GetUpdateImageFormResponse>
    {

    }

    public class GetUpdateImageFormHandler : IRequestHandler<GetUpdateImageFormRequest, GetUpdateImageFormResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUpdateImageFormHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUpdateImageFormResponse> Handle(GetUpdateImageFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<ProjectImage>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Image.NotFound.Message);

            var image = new FormFile
            {
                ContentType = entity.ContentType,
                FileName = entity.FileName,
            };

            return new GetUpdateImageFormResponse
            {
                Form = new UpdateImageRequest
                {
                    Id = entity.Id,
                    Image = image,
                    Data = entity.Data
                }
            };
        }
    }

    public class GetUpdateImageFormResponse
    {
        public UpdateImageRequest Form { get; set; }
    }
}

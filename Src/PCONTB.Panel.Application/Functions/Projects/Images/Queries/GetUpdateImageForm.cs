using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Images;

namespace PCONTB.Panel.Application.Functions.Projects.Images.Queries
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
            var entity = await _dbContext.Set<Image>().FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Image.NotFound.Message);

        }
    }

    public class GetUpdateImageFormResponse
    {
    }
}

using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Functions.Location.Countries.Commands;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{   
    public class GetUpdateCountryFormRequest : BaseQuery, IRequest<GetUpdateCountryFormResponse>
    {

    }

    public class GetUpdateCountryFormHandler : IRequestHandler<GetUpdateCountryFormRequest, GetUpdateCountryFormResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUpdateCountryFormHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUpdateCountryFormResponse> Handle(GetUpdateCountryFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Country>()
                .FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Country.NotFound.Message);

            return new GetUpdateCountryFormResponse
            {
                Form = new UpdateCountryRequest
                {
                    Id = entity.Id,
                    Name = entity.Name,
                }
            };
        }
    }

    public class GetUpdateCountryFormResponse
    {
        public UpdateCountryRequest Form { get; set; }
    }
}

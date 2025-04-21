using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Select;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class SelectCountriesRequest : BaseQuery, IRequest<SelectResponse>
    {
    }

    public class SelectCountriesHandler : IRequestHandler<SelectCountriesRequest, SelectResponse>
    {
        private readonly IApplicationDbContext _context;

        public SelectCountriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SelectResponse> Handle(SelectCountriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Country>()
                .ToListAsync(cancellationToken);

            return new SelectResponse
            {
                Data = [.. entity.Select(m => new SelectData(m.Id, m.Name))],
            };
        }
    }
}

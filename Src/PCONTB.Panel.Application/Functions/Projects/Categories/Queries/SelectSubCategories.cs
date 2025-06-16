using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Select;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class SelectSubCategoriesRequest : BaseQuery, IRequest<SelectResponse>
    {
    }

    public class SelectSubCategoriesHandler : IRequestHandler<SelectSubCategoriesRequest, SelectResponse>
    {
        private readonly IApplicationDbContext _context;

        public SelectSubCategoriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SelectResponse> Handle(SelectSubCategoriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<Subcategory>()
                .Where(m => m.CategoryId == request.Id)
                .ToListAsync(cancellationToken);

            return new SelectResponse
            {
                Data = [.. entity.Select(m => new SelectData(m.Id, m.Name))],
            };
        }
    }
}

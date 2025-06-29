using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Locations.Countries;
using PCONTB.Panel.Application.Models.Projects.Categories;
using PCONTB.Panel.Application.Table;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
    {
        public class CountryPagedQueryHandler : PagedQueryHandler<Category, CategoryTableDto>
        {
            private readonly IApplicationDbContext _dbContext;

            public CountryPagedQueryHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public override async Task<PagedResultDto<CategoryTableDto>> Handle(PagedQueryRequest<CategoryTableDto> request, CancellationToken cancellationToken)
            {
                var query = GetQuery();

                SetQuery(query);

                return await base.Handle(request, cancellationToken);
            }

            protected override IQueryable<Category> GetQuery()
            {
                return _dbContext.Set<Category>();
            }

            protected override string[] GetGlobalSearchProperties()
            {
                return new[] { "Name" };
            }

            protected override CategoryTableDto MapEntityToDto(Category country)
            {
                return new CategoryTableDto
                {
                    Id = country.Id,
                    Name = country.Name
                };
            }
        }
    }
}

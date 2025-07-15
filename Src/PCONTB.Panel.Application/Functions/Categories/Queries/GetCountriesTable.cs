using PCONTB.Panel.Application.Common.Functions.Tables;
using PCONTB.Panel.Application.Models.Categories;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
    {
        public class CountryPagedQueryHandler(IUnitOfWork unitOfWork) : PagedQueryHandler<Category, CategoryTableDto>
        {
            public override async Task<PagedResultDto<CategoryTableDto>> Handle(PagedQueryRequest<CategoryTableDto> request, CancellationToken cancellationToken)
            {
                var query = GetQuery();

                SetQuery(query);

                return await base.Handle(request, cancellationToken);
            }

            protected override IQueryable<Category> GetQuery()
            {
                return unitOfWork.CategoryRepository.GetQuery();
            }

            protected override string[] GetGlobalSearchProperties()
            {
                return new[] { "Name", "Enabled" };
            }

            protected override CategoryTableDto MapEntityToDto(Category country)
            {
                return new CategoryTableDto
                {
                    Id = country.Id,
                    Name = country.Name,
                    Enabled = country.Enabled
                };
            }
        }
    }
}

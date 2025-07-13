using PCONTB.Panel.Application.Models.Projects.Categories;
using PCONTB.Panel.Application.Table;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
    {
        public class CountryPagedQueryHandler : PagedQueryHandler<Category, CategoryTableDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CountryPagedQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public override async Task<PagedResultDto<CategoryTableDto>> Handle(PagedQueryRequest<CategoryTableDto> request, CancellationToken cancellationToken)
            {
                var query = GetQuery();

                SetQuery(query);

                return await base.Handle(request, cancellationToken);
            }

            protected override IQueryable<Category> GetQuery()
            {
                return _unitOfWork.CategoryRepository.GetQuery();
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

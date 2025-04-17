using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Dto.Categories;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class GetAllCategoriesRequest : IRequest<GetAllCategoriesResponse>
    {
    }

    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetAllCategoriesHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Category>()
                .Include(m => m.Subcategories)
                .ToListAsync(cancellationToken);

            return new GetAllCategoriesResponse
            {
                Categories = [.. entity.Select(CategoryDto.Map)]
            };
        }
    }

    public class GetAllCategoriesResponse
    {
        public List<CategoryDto> Categories { get; set; }
    }
}
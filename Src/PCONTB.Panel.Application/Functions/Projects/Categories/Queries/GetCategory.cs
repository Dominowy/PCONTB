using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class GetCategoryRequest : BaseQuery, IRequest<GetCategoryResponse>
    {

    }

    public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, GetCategoryResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCategoryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Category>()
                .Include(m => m.Subcategories)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Category.NotFound.Message);

            return new GetCategoryResponse
            {
                Category = CategoryDto.Map(entity)
            };
        }
    }

    public class GetCategoryResponse
    {
        public CategoryDto Category { get; internal set; }
    }
}
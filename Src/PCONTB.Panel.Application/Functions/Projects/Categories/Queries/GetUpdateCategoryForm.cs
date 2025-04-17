using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Functions.Projects.Categories.Commands;
using PCONTB.Panel.Application.Models.Dto.Categories;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Queries
{
    public class GetUpdateCategoryFormRequest : BaseQuery, IRequest<GetUpdateCategoryFormResponse>
    {

    }

    public class GetUpdateCategoryFormHandler : IRequestHandler<GetUpdateCategoryFormRequest, GetUpdateCategoryFormResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUpdateCategoryFormHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUpdateCategoryFormResponse> Handle(GetUpdateCategoryFormRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Category>()
                .Include(m => m.Subcategories)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Category.NotFound.Message);

            return new GetUpdateCategoryFormResponse
            {
                Form = new UpdateCategoryRequest
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Subcategories = [.. entity.Subcategories.Select(SubcategoryDto.Map)]
                }
            };
        }
    }

    public class GetUpdateCategoryFormResponse
    {
        public UpdateCategoryRequest Form { get; set; }
    }
}

using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Commands
{
    public class UpdateCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        public List<SubcategoryDto> Subcategories { get; set; }
    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateCategoryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Category>()
                .Include(m => m.Subcategories)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Category.NotFound.Message);

            entity.SetName(request.Name);

            var toRemove = entity.Subcategories
                .Where(existing => !request.Subcategories.Any(x => x.Id == existing.Id))
                .ToList();

            foreach (var sub in toRemove)
            {
                _dbContext.Set<CategorySubcategory>().Remove(sub);
            }

            foreach (var subcategory in request.Subcategories)
            {
                if (subcategory.Id != Guid.Empty)
                {
                    var existing = entity.Subcategories.FirstOrDefault(x => x.Id == subcategory.Id);
                    if (existing != null)
                    {
                        existing.SetName(subcategory.Name);
                    }
                }
                else
                {
                    await _dbContext.Set<CategorySubcategory>().AddAsync(SubcategoryDto.Map(subcategory), cancellationToken);
                }
            }


            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateCategoryValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Category.NameEmpty.Message)
                .MustAsync(async(s, c, ct) => await CheckNameIsUnique(s.Id, c, ct)).WithMessage(ErrorCodes.Category.NameExist.Message);
        }

        private async Task<bool> CheckNameIsUnique(Guid id, string name, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<Category>().AnyAsync(m => m.Name == name && m.Id != id, cancellationToken);
        }
    }
}

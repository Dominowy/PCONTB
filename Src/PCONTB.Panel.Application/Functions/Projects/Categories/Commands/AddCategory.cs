using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Models.Dto.Categories;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Commands
{
    public class AddCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        public List<SubcategoryDto> Subcategories { get; set; }
    }

    public class AddCategoryHandler : IRequestHandler<AddCategoryRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddCategoryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = new Category(request.Name);

            var subCategoryToAdd = request.Subcategories.Select(m => SubcategoryDto.Map(m, entity.Id)).ToList();

            entity.SetSubcategories(subCategoryToAdd);

            await _dbContext.Set<Category>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);

        }
    }

    public class AddCategoryValidator : AbstractValidator<AddCategoryRequest>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddCategoryValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Category.NameEmpty.Message)
                .MustAsync(CheckNameIsUnique).WithMessage(ErrorCodes.Category.NameExist.Message);
            
        }

        private async Task<bool> CheckNameIsUnique(string name, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<Category>().AnyAsync(m => m.Name == name, cancellationToken);
        }
    }
}
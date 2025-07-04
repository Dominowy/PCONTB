using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Application.Models.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Commands
{
    public class AddCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        public List<SubcategoryDto> Subcategories { get; set; }
    }

    public class AddCategoryHandler : IRequestHandler<AddCategoryRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = new Category(request.Name);

            var subCategoryToAdd = request.Subcategories.Select(m => SubcategoryDto.Map(m, entity.Id)).ToList();

            entity.SetSubcategories(subCategoryToAdd);

            await _unitOfWork.CategoryRepository.Add(entity, cancellationToken);
            await _unitOfWork.Save(cancellationToken);

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
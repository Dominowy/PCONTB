using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Commands
{
    public class AddCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        public bool Enabled { get; set; }
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
            var aggregate = new Category(request.Name, request.Enabled);

            await _unitOfWork.CategoryRepository.Add(aggregate, cancellationToken);
            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(aggregate.Id);

        }
    }

    public class AddCategoryValidator : AbstractValidator<AddCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Categories.Category.NameEmpty.Message)
                .MustAsync(CheckNameIsUnique).WithMessage(ErrorCodes.Categories.Category.NameExist.Message);
            
        }

        private async Task<bool> CheckNameIsUnique(string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.CategoryRepository.Exist(m => m.Name == name, cancellationToken);
        }
    }
}
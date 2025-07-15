using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Commands
{
    public class DeleteCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        
    }

    public class DeleteCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Categories.Category.NotFound.Message);

            await unitOfWork.CategoryRepository.Delete(entity, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Id)
                .NotEmpty().WithMessage(ErrorCodes.Categories.Category.NameEmpty.Message)
                .MustAsync(async (s, c, ct) => await CheckProjectExist(s.Id, ct)).WithMessage(ErrorCodes.Categories.Category.ProjectExist.Message);
        }

        private async Task<bool> CheckProjectExist(Guid id, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetBy(m => m.Id == id, cancellationToken);

            return category.Projects.Count == 0;
        }
    }
}

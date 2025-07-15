using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Commands
{
    public class UpdateCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Categories.Category.NotFound.Message);

            entity.SetName(request.Name);

            await _unitOfWork.CategoryRepository.Update(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Categories.Category.NameEmpty.Message)
                .MustAsync(async(s, c, ct) => await CheckNameIsUnique(s.Id, c, ct)).WithMessage(ErrorCodes.Categories.Category.NameExist.Message);
        }

        private async Task<bool> CheckNameIsUnique(Guid id, string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.CategoryRepository.Exist(m => m.Name == name && m.Id != id, cancellationToken);
        }
    }
}

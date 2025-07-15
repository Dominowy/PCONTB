using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Categories.Commands
{
    public class UpdateCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        public bool Enabled { get; set; }
    }

    public class UpdateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Categories.Category.NotFound.Message);
            
            aggregate.SetName(request.Name);
            aggregate.SetEnabled(request.Enabled);

            await unitOfWork.CategoryRepository.Update(aggregate, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(aggregate.Id);
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

using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Commands
{
    public class DeleteCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Category.NotFound.Message);

            await _unitOfWork.CategoryRepository.Delete(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Categories.Commands
{
    public class DeleteCategoryRequest : BaseCommand, IRequest<CommandResult>
    {
        
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteCategoryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Category>()
                .FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Category.NotFound.Message);

            _dbContext.Set<Category>().Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Commands
{
    public class DeleteCountryRequest : BaseCommand, IRequest<CommandResult>
    {

    }

    public class DeleteCountryHandler : IRequestHandler<DeleteCountryRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteCountryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(DeleteCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Country>()
                .FindAsync(request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(ErrorCodes.Country.NotFound.Message);

            _dbContext.Set<Country>().Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }
}

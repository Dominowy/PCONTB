using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Application.Functions.Projects.Categories.Commands;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Commands
{
    public class UpdateCountryRequest : BaseCommand, IRequest<CommandResult>
    {

    }

    public class UpdateCountryHandler : IRequestHandler<UpdateCountryRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateCountryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(UpdateCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<Country>()
                .FindAsync(request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Country.NotFound.Message);

            entity.SetName(request.Name);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateCountryValidator : AbstractValidator<UpdateCountryRequest>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateCountryValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Country.NameEmpty.Message)
                .MustAsync(async (s, c, ct) => await CheckNameIsUnique(s.Id, c, ct)).WithMessage(ErrorCodes.Country.NameExist.Message);
        }

        private async Task<bool> CheckNameIsUnique(Guid id, string name, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<Country>().AnyAsync(m => m.Name == name && m.Id != id, cancellationToken);
        }
    }
}

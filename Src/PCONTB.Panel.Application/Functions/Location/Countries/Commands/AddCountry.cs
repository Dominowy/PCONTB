using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.DbContext;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Commands
{
    public class AddCountryRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class AddCountryHandler : IRequestHandler<AddCountryRequest, CommandResult>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddCountryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(AddCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = new Country(request.Name);

            await _dbContext.Set<Country>().AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class AddCountryValidator : AbstractValidator<AddCountryRequest>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddCountryValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Country.NameEmpty.Message)
                .MustAsync(CheckNameIsUnique).WithMessage(ErrorCodes.Country.NameExist.Message);
        }

        private async Task<bool> CheckNameIsUnique(string name, CancellationToken cancellationToken)
        {
            return !await _dbContext.Set<Country>().AnyAsync(m => m.Name == name, cancellationToken);
        }
    }
}
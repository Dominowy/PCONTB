using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Commands
{
    public class AddCountryRequest : BaseCommand, IRequest<CommandResult>
    {
    }

    public class AddCountryHandler : IRequestHandler<AddCountryRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(AddCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = new Country(request.Name);

            await _unitOfWork.CountryRepository.Add(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

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
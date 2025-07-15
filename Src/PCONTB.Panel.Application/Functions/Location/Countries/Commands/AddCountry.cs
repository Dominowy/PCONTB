using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Commands
{
    public class AddCountryRequest : BaseCommand, IRequest<CommandResult>
    {
        public bool Enabled { get; set; }
    }

    public class AddCountryHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddCountryRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(AddCountryRequest request, CancellationToken cancellationToken)
        {
            var aggregate = new Country(request.Name, request.Enabled);

            await unitOfWork.CountryRepository.Add(aggregate, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(aggregate.Id);
        }
    }

    public class AddCountryValidator : AbstractValidator<AddCountryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCountryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Countries.Country.NameEmpty.Message)
                .MustAsync(CheckNameIsUnique).WithMessage(ErrorCodes.Countries.Country.NameExist.Message);
        }

        private async Task<bool> CheckNameIsUnique(string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.CountryRepository.Exist(m => m.Name == name, cancellationToken);
        }
    }
}
using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Models.Codes;
using PCONTB.Panel.Application.Common.Models.Function;
using PCONTB.Panel.Application.Common.Models.Result;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Commands
{
    public class UpdateCountryRequest : BaseCommand, IRequest<CommandResult>
    {

    }

    public class UpdateCountryHandler : IRequestHandler<UpdateCountryRequest, CommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(UpdateCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CountryRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (entity is null) throw new NotFoundException(ErrorCodes.Countries.Country.NotFound.Message);

            entity.SetName(request.Name);

            await _unitOfWork.CountryRepository.Update(entity, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class UpdateCountryValidator : AbstractValidator<UpdateCountryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCountryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage(ErrorCodes.Countries.Country.NameEmpty.Message)
                .MustAsync(async (s, c, ct) => await CheckNameIsUnique(s.Id, c, ct)).WithMessage(ErrorCodes.Countries.Country.NameExist.Message);
        }

        private async Task<bool> CheckNameIsUnique(Guid id, string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.CountryRepository.Exist(m => m.Name == name && m.Id != id, cancellationToken);
        }
    }
}

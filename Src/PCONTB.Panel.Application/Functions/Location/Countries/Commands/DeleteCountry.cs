using FluentValidation;
using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Functions.Categories.Commands;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Commands
{
    public class DeleteCountryRequest : BaseCommand, IRequest<CommandResult>
    {

    }

    public class DeleteCountryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCountryRequest, CommandResult>
    {
        public async Task<CommandResult> Handle(DeleteCountryRequest request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CountryRepository.GetBy(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(ErrorCodes.Countries.Country.NotFound.Message);

            await unitOfWork.CountryRepository.Delete(entity, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return new CommandResult(entity.Id);
        }
    }

    public class DeleteCountryValidator : AbstractValidator<DeleteCountryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCountryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Id)
                .NotEmpty().WithMessage(ErrorCodes.Countries.Country.NameEmpty.Message)
                .MustAsync(async (s, c, ct) => await CheckProjectExist(s.Id, ct)).WithMessage(ErrorCodes.Countries.Country.ProjectExist.Message);
        }

        private async Task<bool> CheckProjectExist(Guid id, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CountryRepository.GetBy(m => m.Id == id, cancellationToken);

            return category.Projects.Count == 0;
        }
    }
}

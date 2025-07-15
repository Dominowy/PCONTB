using MediatR;
using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Common.Functions;
using PCONTB.Panel.Application.Functions.Location.Countries.Commands;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{   
    public class GetUpdateCountryFormRequest : BaseQuery, IRequest<GetUpdateCountryFormResponse>
    {

    }

    public class GetUpdateCountryFormHandler : IRequestHandler<GetUpdateCountryFormRequest, GetUpdateCountryFormResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUpdateCountryFormHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUpdateCountryFormResponse> Handle(GetUpdateCountryFormRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await _unitOfWork.CountryRepository.GetBy(m => m.Id == request.Id, cancellationToken);

            if (aggregate is null) throw new NotFoundException(ErrorCodes.Countries.Country.NotFound.Message);

            return new GetUpdateCountryFormResponse
            {
                Form = new UpdateCountryRequest
                {
                    Id = aggregate.Id,
                    Name = aggregate.Name,
                    Enabled = aggregate.Enabled
                }
            };
        }
    }

    public class GetUpdateCountryFormResponse
    {
        public UpdateCountryRequest Form { get; set; }
    }
}

using MediatR;
using PCONTB.Panel.Application.Functions.Location.Countries.Commands;

namespace PCONTB.Panel.Application.Functions.Location.Countries.Queries
{
    public class GetAddCountryFormRequest : IRequest<GetAddCountryFormResponse>
    {

    }

    public class GetAddCountryFormHandler : IRequestHandler<GetAddCountryFormRequest, GetAddCountryFormResponse>
    {
        public async Task<GetAddCountryFormResponse> Handle(GetAddCountryFormRequest request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new GetAddCountryFormResponse
            {
                Form = new AddCountryRequest()
            });
        }
    }
    public class GetAddCountryFormResponse
    {
        public AddCountryRequest Form { get; set; }
    }
}
using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Models.Locations.Countries
{
    public class CountryDto : EntityDto
    {
        public string Name { get; set; }

        public static CountryDto Map(Country item)
        {
            return new CountryDto
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}

using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Location.Countries;

namespace PCONTB.Panel.Application.Models.Dto.Locations.Countries
{
    public class CountryDto : EntityDto
    {
        public string Name { get; set; }

        public static CountryDto Map(Country item)
        {
            return new CountryDto
            {
                Name = item.Name
            };
        }
    }
}

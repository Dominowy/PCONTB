using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Application.Models.Dto.Account.Users;
using PCONTB.Panel.Application.Models.Dto.Categories;
using PCONTB.Panel.Application.Models.Dto.Locations.Countries;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Models.Dto.Projects.Projects
{
    public class ProjectDto : EntityDto
    {
        public string Name { get; set; }
        public UserDto User { get; set; }
        public CountryDto Country { get; set; }
        public CategoryDto Category { get; set; }
        public SubcategoryDto? Subcategory { get; set; }

        internal static ProjectDto Map(Project entity)
        {
            return new ProjectDto
            { 
                Id = entity.Id,
                Name = entity.Name, 
                User = UserDto.Map(entity.User),
                Country = CountryDto.Map(entity.Country),
                Category = CategoryDto.Map(entity.Category),
                Subcategory = SubcategoryDto.Map(entity.Subcategory)
            };
        }
    }
}

using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Functions.Projects.Projects.Models
{
    public class CategoryDto : EntityDto
    {
        public string Name { get; set; }

        public static CategoryDto Map(Category item)
        {
            return new CategoryDto
            {
                Id = item.Id,
                Name = item.Name,
            };
        }
    }
}

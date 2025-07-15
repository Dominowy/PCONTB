using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Categories;

namespace PCONTB.Panel.Application.Models.Categories
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

using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Models.Dto.Categories
{
    public class SubcategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public static SubcategoryDto Map(Subcategory item)
        {
            return new SubcategoryDto
            {
                Id = item.Id,
                Name = item.Name,
                CategoryId = item.CategoryId
            };
        }
        public static Subcategory Map(SubcategoryDto item)
        {
            return new Subcategory(item.Name, item.CategoryId);
        }

        public static Subcategory Map(SubcategoryDto item, Guid categoryId)
        {
            return new Subcategory(item.Name, categoryId);
        }
    }
}

using PCONTB.Panel.Domain.Categories;

namespace PCONTB.Panel.Application.Models.Projects.Categories
{
    public class SubcategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public static SubcategoryDto Map(CategorySubcategory item)
        {
            if (item is null) return null;

            return new SubcategoryDto
            {
                Id = item.Id,
                Name = item.Name,
                CategoryId = item.CategoryId
            };
        }
        public static CategorySubcategory Map(SubcategoryDto item)
        {
            return new CategorySubcategory(item.Name, item.CategoryId);
        }

        public static CategorySubcategory Map(SubcategoryDto item, Guid categoryId)
        {
            return new CategorySubcategory(item.Name, categoryId);
        }
    }
}

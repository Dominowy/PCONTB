using PCONTB.Panel.Domain.Projects.Categories;

namespace PCONTB.Panel.Application.Models.Projects.Categories
{
    public class SubcategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public static SubcategoryDto Map(Subcategory item)
        {
            if (item is null) return null;

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

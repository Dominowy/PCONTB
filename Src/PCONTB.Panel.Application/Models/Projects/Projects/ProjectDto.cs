using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Models.Projects.Projects
{
    public class ProjectDto : EntityDto
    {
        public string Name { get; set; }
        public UserDto User { get; set; }
        public NameRelatedDto Country { get; set; }
        public NameRelatedDto Category { get; set; }
        public NameRelatedDto? Subcategory { get; set; }

        public static ProjectDto Map(Project entity)
        {
            return new ProjectDto
            { 
                Id = entity.Id,
                Name = entity.Name, 
                User = UserDto.Map(entity.User),
                Country = NameRelatedDto.Map(entity.Country),
                Category = NameRelatedDto.Map(entity.Category),
                Subcategory = NameRelatedDto.Map(entity.Subcategory)
            };
        }
    }
}

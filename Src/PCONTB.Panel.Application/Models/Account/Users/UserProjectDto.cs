using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Application.Models.Account.Users
{
    public class UserProjectDto : EntityDto
    {
        public string Name { get; set; }
        public NameRelatedDto Country { get; set; }
        public NameRelatedDto Category { get; set; }

        public static UserProjectDto Map(Project entity)
        {
            return new UserProjectDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Country = NameRelatedDto.Map(entity.Country),
                Category = NameRelatedDto.Map(entity.Category),
            };
        }
    }
}

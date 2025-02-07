using PCONTB.Panel.Application.Models.Dto.Account.Users;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Application.Models.Dto.Projects.Projects
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserDto User { get; set; }

        internal static ProjectDto Map(Project entity)
        {
            return new ProjectDto 
            { 
                Id = entity.Id,
                Name = entity.Name, 
                User = UserDto.Map(entity.User) 
            };
        }
    }
}

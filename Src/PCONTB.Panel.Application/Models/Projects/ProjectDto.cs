using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Application.Common.Models;
using PCONTB.Panel.Domain.Projects;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class ProjectDto : EntityDto
    {
        public string Name { get; set; }
        public ProjectUserDto User { get; set; }
        public NameRelatedDto Country { get; set; }
        public NameRelatedDto Category { get; set; }
        public FormFile Image { get; set; }
        public byte[] ImageData { get; set; }
        public FormFile Video { get; set; }
        public byte[] VideoData { get; set; }
        public List<ProjectCollaboratorDto> Collaborators { get; set; }

        public static ProjectDto Map(Project entity)
        {
            return new ProjectDto
            {
                Id = entity.Id,
                Name = entity.Name,
                User = ProjectUserDto.Map(entity.User),
                Country = NameRelatedDto.Map(entity.Country),
                Category = NameRelatedDto.Map(entity.Category),
                Collaborators = [.. entity.Collaborators.Select(ProjectCollaboratorDto.Map)]
            };
        }
    }
}

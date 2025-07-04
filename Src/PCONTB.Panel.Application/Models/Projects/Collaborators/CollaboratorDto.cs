using PCONTB.Panel.Application.Common.Models.Dto;
using PCONTB.Panel.Application.Models.Account.Users;
using PCONTB.Panel.Domain.Projects.Projects.Collaborators;

namespace PCONTB.Panel.Application.Models.Projects.Collaborators
{
    public class CollaboratorDto : EntityDto
    {
        public UserDto User { get; set; }
        public static CollaboratorDto Map(ProjectCollaborator m)
        {
            return new CollaboratorDto
            {
                Id = m.Id,
                User = UserDto.Map(m.User)
            };
        }
    }
}

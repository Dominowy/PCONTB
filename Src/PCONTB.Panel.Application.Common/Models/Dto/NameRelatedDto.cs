using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Application.Common.Models.Dto
{
    public class NameRelatedDto : EntityDto
    {
        public string Name { get; set; }

        public static NameRelatedDto Map(EntityName entity)
        {
            if (entity == null) return null;

            return new NameRelatedDto 
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}

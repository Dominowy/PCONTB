using PCONTB.Panel.Domain.Common;

namespace PCONTB.Panel.Application.Common.Models
{
    public class NameRelatedDto : IEntity, IHasName
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public static NameRelatedDto Map<T>(T entity)
           where T : IEntity, IHasName
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

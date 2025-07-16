using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Campaign;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Domain.Projects
{
    public class Project : BaseAggregateNameEnabled
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public Guid CountryId { get; private set; }
        public virtual Country Country { get; private set; }

        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; }

        public virtual List<ProjectCollaborator> Collaborators { get; private set; } = new List<ProjectCollaborator>();

        public string? Description { get; private set; }

        public Guid? ImageId { get; private set; }
        public virtual ProjectImage? Image { get; private set; }

        public Guid? VideoId { get; private set; }
        public virtual ProjectVideo? Video { get; private set; }

        public Guid? ProjectCampaingId { get; private set; }
        public virtual ProjectCampaign? ProjectCampaing { get; private set; }
        protected Project() : base()
        {
        }

        public Project(Guid id, string name, Guid userId) : base(id)
        {
            SetName(name);
            SetEnabled(true);
            UserId = userId;
        }

        public Project(Guid id, string name, Guid userId, Guid countryId, Guid categoryId) : base(id)
        {
            SetName(name);
            SetEnabled(true);
            UserId = userId;
            CountryId = countryId;
            CategoryId = categoryId;
        }

        public void SetUser(Guid userId)
        {
            var anyChange = UserId != userId;
            if (!anyChange) return;

            UserId = userId;
        }

        public void SetCountry(Guid countryId)
        {
            var anyChange = CountryId != countryId;
            if (!anyChange) return;

            CountryId = countryId;
        }

        public void SetCategory(Guid categoryId)
        {
            var anyChange = CategoryId != categoryId;
            if (!anyChange) return;

            CategoryId = categoryId;
        }

        public void SetImage(ProjectImage image)
        {
            var anyChange = Image != image;
            if (!anyChange) return;

            Image = image;
        }

        public void SetVideo(ProjectVideo video)
        {
            var anyChange = Video != video;
            if (!anyChange) return;

            Video = video;
        }

        public void AddCollaborator(ProjectCollaborator entity)
        {
            if (entity == null) return;

            Collaborators.Add(entity);
        }

        public void RemoveCollaborator(ProjectCollaborator entity)
        {
            if (entity == null) return;

            Collaborators.Remove(entity);
        }
    }
}
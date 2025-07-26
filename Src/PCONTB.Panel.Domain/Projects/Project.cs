using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Account.Users.Favorites;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Campaigns;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Communites;
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

        public virtual List<ProjectCollaborator> Collaborators { get; private set; } = [];
        public string? Description { get; private set; }

        public Guid? ImageId { get; private set; }
        public virtual ProjectImage? Image { get; private set; }

        public Guid? VideoId { get; private set; }
        public virtual ProjectVideo? Video { get; private set; }

        public Guid? CampaingId { get; private set; }
        public virtual ProjectCampaign? Campaing { get; private set; }

        public Guid? CommunityId { get; private set; }
        public virtual ProjectCommunity? Community { get; private set; }

        public virtual List<UserProjectFavorite> Favorites { get; private set; } = [];

        public int Views { get; private set; } = 0;

        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        protected Project() : base()
        {
        }

        public Project(Guid id, string name, Guid userId) : base(id)
        {
            SetName(name);
            SetEnabled(true);
            UserId = userId;
            Created = DateTime.UtcNow;
        }

        public Project(Guid id, string name, Guid userId, Guid countryId, Guid categoryId) : base(id)
        {
            SetName(name);
            SetEnabled(true);
            UserId = userId;
            CountryId = countryId;
            CategoryId = categoryId;
            Created = DateTime.UtcNow;
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
            Modified = DateTime.UtcNow;
        }

        public void SetCategory(Guid categoryId)
        {
            var anyChange = CategoryId != categoryId;
            if (!anyChange) return;

            CategoryId = categoryId;
            Modified = DateTime.UtcNow;
        }

        public void SetImage(ProjectImage image)
        {
            var anyChange = Image != image;
            if (!anyChange) return;

            Image = image;
            Modified = DateTime.UtcNow;
        }

        public void SetVideo(ProjectVideo video)
        {
            var anyChange = Video != video;
            if (!anyChange) return;

            Video = video;
            Modified = DateTime.UtcNow;
        }

        public void AddCollaborator(ProjectCollaborator entity)
        {
            if (entity == null) return;

            Collaborators.Add(entity);
            Modified = DateTime.UtcNow;
        }

        public void RemoveCollaborator(ProjectCollaborator entity)
        {
            if (entity == null) return;

            Collaborators.Remove(entity);
            Modified = DateTime.UtcNow;
        }

        public void SetProjectCampaing(ProjectCampaign campaign)
        {
            var anyChange = Campaing != campaign;
            if (!anyChange) return;

            Campaing = campaign;
            Modified = DateTime.UtcNow;
        }

        public void UpdateViews()
        {
            Views++;
        }
    }
}
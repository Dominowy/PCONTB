using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Domain.Projects.Projects
{
    public class Project : BaseAggregateNameEnabled
    {
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public Guid CountryId { get; private set; }
        public virtual Country Country { get; private set; }

        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; }

        public Guid? SubcategoryId { get; private set; }
        public virtual Subcategory? Subcategory { get; private set; }

        public virtual List<Collaborator> Collaborators { get; private set; }

        public string? Description { get; private set; }

        public Guid? ImageId { get; private set; }
        public virtual ProjectImage? Image { get; private set; }

        public Guid? VideoId { get; private set; }
        public virtual ProjectVideo? Video { get; private set; }

        protected Project() : base()
        {
        }

        public Project(Guid id, string name, Guid userId) : base(id)
        {
            SetName(name);
            SetEnabled(true);
            UserId = userId;
        }

        public Project(Guid id, string name, Guid userId, Guid countryId, Guid categoryId, Guid? subcategoryId) : base(id)
        {
            SetName(name);
            SetEnabled(true);
            UserId = userId;
            CountryId = countryId;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
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

        public void SetSubcategory(Guid? subcategoryId)
        {
            var anyChange = SubcategoryId != subcategoryId;
            if (!anyChange) return;

            SubcategoryId = subcategoryId;
        }

        public void SetImageId(Guid? imageId)
        {
            var anyChange = ImageId != imageId;
            if (!anyChange) return;

            ImageId = imageId;
        }

        public void SetVideoId(Guid? videoId)
        {
            var anyChange = VideoId != videoId;
            if (!anyChange) return;

            VideoId = videoId;
        }
    }
}
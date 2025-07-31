using PCONTB.Panel.Domain.Common;
using PCONTB.Panel.Domain.Community;

namespace PCONTB.Panel.Domain.Projects.Communites
{
    public class ProjectCommunity : BaseEntityEnabled
    {
        public virtual List<Message> Messages { get; private set; } = [];
    }
}

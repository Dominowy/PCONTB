using PCONTB.Panel.Domain.Community;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure.Repositories
{
    public class MessageRepository(ApplicationDbContext context) : Repository<Message>(context), IMessageRepository
    {
    }
}

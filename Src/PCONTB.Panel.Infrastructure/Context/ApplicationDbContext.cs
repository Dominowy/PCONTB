using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.User;
using System.Security.Cryptography.X509Certificates;

namespace PCONTB.Panel.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        
    }
}

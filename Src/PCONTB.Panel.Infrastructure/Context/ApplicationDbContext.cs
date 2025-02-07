using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.DbContext;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(u => u.UserId);
        }
    }
}

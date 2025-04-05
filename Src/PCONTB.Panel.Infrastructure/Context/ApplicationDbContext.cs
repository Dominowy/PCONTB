using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.DbContext;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects;

namespace PCONTB.Panel.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<User> User { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Subcategory> SubCategory { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasOne(m => m.User)
                .WithMany(m => m.Projects)
                .HasForeignKey(m => m.UserId);

            builder.Entity<Subcategory>()
                .HasOne(m => m.Category)
                .WithMany(m => m.Subcategories)
                .HasForeignKey(m => m.CategoryId);

            builder.Entity<Subcategory>()
                .HasOne(m => m.Category)
                .WithMany(m => m.Subcategories)
                .HasForeignKey(m => m.CategoryId);

            builder.Entity<Collaborator>();
        }
    }
}

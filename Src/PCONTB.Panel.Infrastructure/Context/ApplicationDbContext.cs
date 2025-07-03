using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Application.Contracts.Infrastructure.Persistance;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects.Categories;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Projects;
using PCONTB.Panel.Domain.Projects.Projects.Files;

namespace PCONTB.Panel.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Session> Session { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<ProjectImage> ProjectImage { get; set; }
        public DbSet<ProjectVideo> ProjectVideo { get; set; }
        public DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Session>()
                .HasOne(m => m.User)
                .WithMany(m => m.Sessions)
                .HasForeignKey(m => m.UserId);

            builder.Entity<Subcategory>()
                .HasOne(m => m.Category)
                .WithMany(m => m.Subcategories)
                .HasForeignKey(m => m.CategoryId);

            builder.Entity<Collaborator>()
                .HasOne(m => m.User)
                .WithMany(m => m.Collaborators)
                .HasForeignKey(m => m.UserId);

            builder.Entity<Collaborator>()
                .HasOne(m => m.Project)
                .WithMany(m => m.Collaborators)
                .HasForeignKey(m => m.ProjectId);

            builder.Entity<Project>()
                .HasOne(m => m.User)
                .WithMany(m => m.Projects)
                .HasForeignKey(m => m.UserId);

            builder.Entity<Project>()
                .HasOne(m => m.Country)
                .WithMany(m => m.Projects)
                .HasForeignKey(m => m.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Project>()
                .HasOne(m => m.Category)
                .WithMany(m => m.Projects)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Project>()
                .HasOne(m => m.Subcategory)
                .WithMany(m => m.Projects)
                .HasForeignKey(m => m.SubcategoryId);

            builder.Entity<Project>()
                .HasOne(m => m.Image)
                .WithOne()
                .HasForeignKey<Project>(m => m.ImageId)
                .HasPrincipalKey<ProjectImage>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Project>()
                .HasOne(m => m.Video)
                .WithOne()
                .HasForeignKey<Project>(m => m.VideoId)
                .HasPrincipalKey<ProjectVideo>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

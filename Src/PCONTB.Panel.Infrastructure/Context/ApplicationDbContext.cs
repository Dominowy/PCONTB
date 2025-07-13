using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Files;

namespace PCONTB.Panel.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Session> Session { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategorySubcategory> CategorySubcategory { get; set; }
        public DbSet<ProjectCollaborator> ProjectCollaborator { get; set; }
        public DbSet<ProjectImage> ProjectImage { get; set; }
        public DbSet<ProjectVideo> ProjectVideo { get; set; }
        public DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Session>()
                .HasOne(m => m.User)
                .WithMany(m => m.Sessions)
                .HasForeignKey(m => m.UserId);

            builder.Entity<UserRole>()
                .HasOne(m => m.User)
                .WithMany(m => m.UserRoles)
                .HasForeignKey(m => m.UserId);

            builder.Entity<UserRole>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<CategorySubcategory>()
                .HasOne(m => m.Category)
                .WithMany(m => m.Subcategories)
                .HasForeignKey(m => m.CategoryId);

            builder.Entity<CategorySubcategory>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProjectCollaborator>()
                .HasOne(m => m.User)
                .WithMany(m => m.Collaborators)
                .HasForeignKey(m => m.UserId);

            builder.Entity<ProjectCollaborator>()
                .HasOne(m => m.Project)
                .WithMany(m => m.Collaborators)
                .HasForeignKey(m => m.ProjectId);

            builder.Entity<ProjectCollaborator>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

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
                .WithOne(m => m.Project)
                .HasForeignKey<Project>(m => m.ImageId)
                .HasPrincipalKey<ProjectImage>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProjectImage>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Project>()
                .HasOne(m => m.Video)
                .WithOne()
                .HasForeignKey<Project>(m => m.VideoId)
                .HasPrincipalKey<ProjectVideo>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProjectVideo>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();
        }
    }
}

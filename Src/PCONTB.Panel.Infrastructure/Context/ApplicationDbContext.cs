using Microsoft.EntityFrameworkCore;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Account.Users;
using PCONTB.Panel.Domain.Account.Users.Roles;
using PCONTB.Panel.Domain.Account.Users.Wallets;
using PCONTB.Panel.Domain.Categories;
using PCONTB.Panel.Domain.Location.Countries;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Campaigns;
using PCONTB.Panel.Domain.Projects.Collaborators;
using PCONTB.Panel.Domain.Projects.Communites;
using PCONTB.Panel.Domain.Projects.Files;
using System.Reflection.Emit;

namespace PCONTB.Panel.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Session> Session { get; set; }
        public DbSet<UserWallet> UserWallet { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProjectCollaborator> ProjectCollaborator { get; set; }
        public DbSet<ProjectImage> ProjectImage { get; set; }
        public DbSet<ProjectVideo> ProjectVideo { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectCampaign> ProjectCampaign { get; set; }
        public DbSet<ProjectCampaignContent> ProjectCampaignContent { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Session>()
                .HasOne(m => m.User)
                .WithMany(m => m.Sessions)
                .HasForeignKey(m => m.UserId);

            builder.Entity<UserRole>()
                .HasOne(m => m.User)
                .WithMany(m => m.Roles)
                .HasForeignKey(m => m.UserId);

            builder.Entity<UserRole>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<UserWallet>()
                .HasOne(m => m.User)
                .WithMany(m => m.Wallets)
                .HasForeignKey(m => m.UserId);

            builder.Entity<UserWallet>()
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
                .HasOne(m => m.Image)
                .WithOne(m => m.Project)
                .HasForeignKey<Project>(m => m.ImageId)
                .HasPrincipalKey<ProjectImage>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Project>()
                .HasOne(m => m.Community)
                .WithOne(m => m.Project)
                .HasForeignKey<Project>(m => m.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProjectCommunityMessage>()
                .HasOne(m => m.User)
                .WithMany(m => m.Messages)
                .HasForeignKey(m => m.UserId)
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

            builder.Entity<Project>()
                .HasOne(m => m.Campaing)
                .WithOne()
                .HasForeignKey<Project>(m => m.CampaingId)
                .HasPrincipalKey<ProjectCampaign>(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProjectCampaignContent>()
                .HasOne(m => m.Campaign)
                .WithMany(m => m.CampaignContents)
                .HasForeignKey(m => m.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectCampaignContent>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();
        }
    }
}

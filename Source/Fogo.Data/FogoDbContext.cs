using Fogo.Data.Configurations;
using Fogo.Data.Extensions;
using Fogo.Models;
using Microsoft.EntityFrameworkCore;

namespace Fogo.Data {

    public class FogoDbContext : DbContext {

        public FogoDbContext(DbContextOptions<FogoDbContext> options) : base(options) {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<ExecutorModel> Executors { get; set; }
        public DbSet<AdvertiserModel> Advertisers { get; set; }
        public DbSet<AdvertTypeModel> AdvertTypes { get; set; }
        public DbSet<SocialNetworkModel> SocialNetworks { get; set; }
        public DbSet<ExecutorSocialNetworkModel> ExecutorSocialNetworks { get; set; }
        public DbSet<SocialNetworkAdvertTypeModel> SocialNetworkAdvertTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.OnModelCreating();

            new UserConfiguration(modelBuilder.Entity<UserModel>());
            new RoleConfiguration(modelBuilder.Entity<RoleModel>());
            new UserRoleConfiguration(modelBuilder.Entity<UserRoleModel>());
            new ExecutorConfiguration(modelBuilder.Entity<ExecutorModel>());
            new AdvertiserConfiguration(modelBuilder.Entity<AdvertiserModel>());
            new AdvertTypeConfiguration(modelBuilder.Entity<AdvertTypeModel>());
            new SocialNetworkConfiguration(modelBuilder.Entity<SocialNetworkModel>());
            new ExecutorSocialNetworkConfiguration(modelBuilder.Entity<ExecutorSocialNetworkModel>());
            new SocialNetworkAdvertTypeConfiguration(modelBuilder.Entity<SocialNetworkAdvertTypeModel>());
        }
    }
}
using Domain.Configuration;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Vault> Vaults { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<StatementCategory> StatementCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VaultConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(36));
            modelBuilder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(36));
            modelBuilder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(36));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(36));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(36));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(36));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(36));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(36));
        }
    }
}

using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class AgentContext: IdentityDbContext<ApplicationUser>
    {
        public AgentContext(DbContextOptions<AgentContext> options):base(options)
        {

        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Volt> Volts { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<StatementCategory> StatementCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VoltConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}

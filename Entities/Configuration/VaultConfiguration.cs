using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class VaultConfiguration : IEntityTypeConfiguration<Vault>
    {
        public void Configure(EntityTypeBuilder<Vault> builder)
        {
            builder.HasAlternateKey(v => new { v.AgentId, v.Date });
        }
    }
}

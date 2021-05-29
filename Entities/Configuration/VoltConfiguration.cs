using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class VoltConfiguration : IEntityTypeConfiguration<Volt>
    {
        public void Configure(EntityTypeBuilder<Volt> builder)
        {
            builder.HasAlternateKey(v => new { v.AgentId, v.Date });
        }
    }
}

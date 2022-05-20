using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Domain
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var agentContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                try
                {
                    agentContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return host;
        }
    }
}

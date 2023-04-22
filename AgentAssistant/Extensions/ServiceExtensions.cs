using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgentAssistant.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<ApplicationContext>(opts =>
                opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.MigrationsAssembly("AgentAssistant")));
        }

        public static void ConfigureSqliteContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqliteConnection");
            services.AddDbContext<ApplicationContext>(opts =>
                opts.UseSqlite(connectionString, options => options.MigrationsAssembly("AgentAssistant")));
        }

        public static void ConfigureMySQLInAppContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("localdb");
            services.AddDbContext<ApplicationContext>(opts =>
                opts.UseMySql(NormalizeAzureInAppConnectionString(connectionString), ServerVersion.AutoDetect(connectionString), options => options.MigrationsAssembly("AgentAssistant")));
        }

        public static void ConfigureJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                    options.Audience = configuration["Auth0:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<ApplicationUser>(opt => {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
        }

        private static string NormalizeAzureInAppConnectionString(string raw)
        {
            string connection = string.Empty;
            try
            {
                var dict = raw.Split(';')
                    .Where(kvp => kvp.Contains('='))
                    .Select(kvp => kvp.Split(new char[] { '=' }, 2))
                    .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim(), StringComparer.InvariantCultureIgnoreCase);

                var ds = dict["Data Source"];
                var dsa = ds.Split(":");
                connection = $"Server={dsa[0]};Port={dsa[1]};Database={dict["Database"]};Uid={dict["User Id"]};Pwd={dict["Password"]};";
            }
            catch(Exception e)
            {
                throw new Exception("Exception: "+ e.Message);
            }
            return connection;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ELearning.Core.Common;
using ELearning.Infrastructure.Data;
using ELearning.Core.Interfaces;

namespace ELearning.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure SQLite database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection") ?? 
                    "Data Source=ELearning.db",
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
} 
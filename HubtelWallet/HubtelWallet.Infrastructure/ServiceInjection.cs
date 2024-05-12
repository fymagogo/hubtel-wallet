using HubtelWallet.Domain.IRepositories;
using HubtelWallet.Infrastructure.Persistence;
using HubtelWallet.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Infrastructure
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .IncludePersistence(configuration)
                .AddTransient<IRepositoryManager, RepositoryManager>()
                .AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            return services;
        }

        public static IServiceCollection IncludePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database"), e =>
                {
                    e.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    e.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);

                })
#if DEBUG
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
#endif
            );

            return services;
        }

        public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            // Create a new scope to retrieve scoped services
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().InitializeDatabaseAsync(cancellationToken);
        }
    }
}

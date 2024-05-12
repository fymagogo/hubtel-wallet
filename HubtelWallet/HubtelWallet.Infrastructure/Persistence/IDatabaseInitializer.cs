using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Infrastructure.Persistence
{
    public interface IDatabaseInitializer
    {
        Task InitializeDatabaseAsync(CancellationToken cancellationToken = default);
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(AppDbContext crmDbContext, ILogger<DatabaseInitializer> logger) =>
            (_dbContext, _logger) = (crmDbContext, logger);

        public async Task InitializeDatabaseAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    _logger.LogInformation("Applying Migrations to target DB...");
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }

                if (await _dbContext.Database.CanConnectAsync(cancellationToken))
                {
                    _logger.LogInformation("Connection to Database Succeeded.");
                }
                else
                {
                    _logger.LogError("Connection to Database failed...");
                }
            }
        }
    }
}

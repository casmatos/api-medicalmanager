using Microsoft.Extensions.Logging;

namespace MedicalManager.Infrastructure.Context.Initial;

public class InitiateDatabase : IDatabaseInitializer
{
    private readonly ApplicationDatabaseContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<InitiateDatabase> _logger;

    public InitiateDatabase(ApplicationDatabaseContext context,
                            IConfiguration config,
                            ILogger<InitiateDatabase> logger)
    {
        _context = context;
        _config = config;
        _logger = logger;
    }

    public async Task InitializeDatabase(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("InitiateDatabase.InitializeDatabasesAsync");

        await _context.Database.EnsureCreatedAsync(cancellationToken);

        await _context.Database.MigrateAsync(cancellationToken);
    }
}

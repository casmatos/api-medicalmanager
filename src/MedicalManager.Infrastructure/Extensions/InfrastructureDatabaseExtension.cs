using MedicalManager.Infrastructure.Context;
using MedicalManager.Infrastructure.Context.Initial;

namespace MedicalManager.Infrastructure.Extensions;

public static class InfrastructureDatabaseExtension
{
    public static IServiceCollection AddDatabaseApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDatabaseContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DatabaseProvider"), sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable(ApplicationDatabaseContext.DEFAULT_MIGRATIONS_TABLE, ApplicationDatabaseContext.DEFAULT_SCHEMA);
                sqlOptions.MigrationsAssembly(typeof(InfrastructureDatabaseExtension).Assembly.FullName);
            });
        });

        services.AddTransient<IDatabaseInitializer, InitiateDatabase>();

        return services;
    }

    public static async Task InitiateDatabaseApplication(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabase(cancellationToken);

    }
}

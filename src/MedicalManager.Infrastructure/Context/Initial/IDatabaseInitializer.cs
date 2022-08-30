namespace MedicalManager.Infrastructure.Context.Initial;

public interface IDatabaseInitializer
{
    Task InitializeDatabase(CancellationToken cancellationToken = default);
}

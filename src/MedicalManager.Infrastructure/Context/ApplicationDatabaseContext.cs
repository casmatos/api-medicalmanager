namespace MedicalManager.Infrastructure.Context;

public class ApplicationDatabaseContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";
    public const string DEFAULT_MIGRATIONS_TABLE = "_Migrations";

    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
    }

    public DbSet<Medication> Medications { get; set; }
}

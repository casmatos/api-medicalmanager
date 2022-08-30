using MedicalManager.Domain.Repository;
using MedicalManager.Infrastructure.Repository;

namespace MedicalManager.Infrastructure.Extensions;

public static class InfrastructureRepositoryExtension
{
    internal static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IMedicationRepository, MedicationRepository>();

        return services;
    }
}

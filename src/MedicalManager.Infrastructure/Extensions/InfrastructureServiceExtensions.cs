using MedicalManager.Infrastructure.Service.Model;
using MedicalManager.Infrastructure.Service.Model.Base;

namespace MedicalManager.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddServicesModels(this IServiceCollection services)
    {
        services.AddTransient<IMedicationService, MedicationService>();

        return services;
    }
}

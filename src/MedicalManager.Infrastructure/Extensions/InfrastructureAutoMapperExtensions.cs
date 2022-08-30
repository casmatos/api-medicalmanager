namespace MedicalManager.Infrastructure.Extensions;

public static class InfrastructureAutoMapperExtensions
{
    public static IServiceCollection AddAutoMapperProfile(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(InfrastructureAutoMapperExtensions));

        return services;
    }

}

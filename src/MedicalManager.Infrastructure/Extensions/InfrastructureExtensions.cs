using Microsoft.AspNetCore.Builder;

namespace MedicalManager.Infrastructure.Extensions;

public static class InfrastructureExtension
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddMiddleware();
        
        services.AddDatabaseApplication(config);
        services.AddRepository();
        services.AddServicesModels();

        services.AddAutoMapperProfile();

        return services;
    }

    public static IApplicationBuilder UseInfrastructureServices(this IApplicationBuilder app)
    {

        app.UseMiddleware();

        return app;
    }

}

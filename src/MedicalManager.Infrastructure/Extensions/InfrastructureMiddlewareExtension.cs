using MedicalManager.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace MedicalManager.Infrastructure.Extensions;

public static class InfrastructureMiddlewareExtension
{
    internal static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
        services.AddScoped<ExeptionMiddleware>();

        return services;
    }


    internal static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExeptionMiddleware>();

        return app;
    }
}

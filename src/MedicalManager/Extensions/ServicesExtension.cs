using Microsoft.OpenApi.Models;

namespace MedicalManager.Extensions;

public static class ServicesExtension
{
    private const string CorsPolicy = nameof(CorsPolicy);

    public static IServiceCollection AddWebApiHostServices(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseQueryStrings = true)
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState.Select(x => new
                                                                    {
                                                                        field = x.Key,
                                                                        errors = x.Value.Errors
                                                                                        .Select(errorDescription => errorDescription.ErrorMessage)
                                                                                        .ToList()
                                                                    })
                                                    .ToList();
                        var errorResult = new
                        {
                            StatusCode = 400,
                            Error = "One or more validation errors occurred.",
                            Errors = errors
                        };

                        return new BadRequestObjectResult(errorResult);
                    };
                });

        services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "API Medical Manager",
                        Version = "v1.0",
                        Contact = new()
                        {
                            Name = "Camilo Matos",
                            Email = "camilo.matos@api-medicalmanager.com",
                        }
                    });
                });

        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy, policy =>
                                policy.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin());
        });

        return services;
    }

    public static IApplicationBuilder UseWebApiHostServices(this WebApplication app)
    {
        // Using to create custom headers for security reasons
        app.Use(async (context, next) =>
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
                context.Response.Headers.TryAdd("X-Xss-Protection", "1; mode=block"); // Prevent XSS Atack
                context.Response.Headers.TryAdd("Strict-Transport-Security", "max-age=31536000; includeSubDomains");

                return Task.CompletedTask;
            });

            await next();
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(CorsPolicy);

        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }
}


var builder = WebApplication.CreateBuilder(args);

builder.Services
        .AddWebApiHostServices()
        .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseWebApiHostServices()
    .UseInfrastructureServices();

await app.Services
        .InitiateDatabaseApplication();

await app.RunAsync();

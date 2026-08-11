using App.Api.Data.Seeders;
using App.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using App.Api.Middlewares;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    builder.Services.AddControllers();

    builder.Services.Configure<RouteOptions>(options =>
    {
        options.LowercaseUrls = true;
    });

    builder.Services.AddOpenApi();

    builder.Services.InitCustomServices(builder.Configuration);

    var app = builder.Build();

    if (args.Contains("seed"))
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        await DatabaseSeeder.SeedAsync(services);

        return;
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.Theme = ScalarTheme.Default;
        });
    }

    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseSerilogRequestLogging();

    app.UseCors("AllowFrontend");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseAppHealthCheck();

    app.Lifetime.ApplicationStarted.Register(() =>
    {
        Microsoft.Extensions.Logging.ILogger logger = app.Logger;

        foreach (var url in app.Urls)
        {
            logger.LogInformation("Listening on: {Url}", url);
            logger.LogInformation("OpenAPI: {Url}/openapi/v1.json", url);
            logger.LogInformation("Scalar : {Url}/scalar/v1", url);
        }
    });

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
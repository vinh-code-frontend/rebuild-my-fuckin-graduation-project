namespace App.Api.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddAppHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks();
        return services;
    }

    public static WebApplication UseAppHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        return app;
    }
}
namespace App.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection InitCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfigurationServiceExtension(configuration);
        services.AddAppDIServiceExtension();
        services.AddFluentValidationServiceExtension();

        services.AddAppHealthCheck();

        return services;
    }
}

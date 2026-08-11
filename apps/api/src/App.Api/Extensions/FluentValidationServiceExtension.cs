using App.Application;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace App.Api.Extensions;

public static class FluentValidationServiceExtension
{
    public static IServiceCollection AddFluentValidationServiceExtension(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();

        return services;
    }
}

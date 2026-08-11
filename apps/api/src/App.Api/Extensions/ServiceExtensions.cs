using System.Reflection;
using System.Text;
using System.Text.Json;
using App.Api.Data;
using App.Application;
using App.Application.Repositories;
using App.Infrastructure;
using App.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace App.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSettings jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()!;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new
                    {
                        statusCode = StatusCodes.Status401Unauthorized,
                        message = "Unauthorized. Token is missing or invalid."
                    });

                    await context.Response.WriteAsync(result);
                },
                OnForbidden = async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new
                    {
                        statusCode = 403,
                        message = "Forbidden. You do not have permission to access this resource."
                    });

                    await context.Response.WriteAsync(result);
                },
                OnAuthenticationFailed = context =>
                {
                    return Task.CompletedTask;
                }
            };
        });
        return services;
    }
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            });
        });
        return services;
    }
    public static IServiceCollection AddImplementationsFromAssembly(
        this IServiceCollection services,
        Assembly assembly,
        string suffix,
        string namespaceSuffix,
        ServiceLifetime lifetime = ServiceLifetime.Scoped
    )
    {
        var implementationTypes = assembly
            .GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.Name.EndsWith(suffix) &&
                t.Namespace?.EndsWith(namespaceSuffix) == true);

        foreach (var implementationType in implementationTypes)
        {
            var interfaceType = implementationType
                .GetInterfaces()
                .FirstOrDefault(i =>
                    i.Name == $"I{implementationType.Name}");

            if (interfaceType is not null)
            {
                services.Add(new ServiceDescriptor(
                    interfaceType,
                    implementationType,
                    lifetime));
            }
        }
        return services;
    }
    public static IServiceCollection AddApplicationServicesFromAssembly(this IServiceCollection services)
    {
        return services.AddImplementationsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly, "Service", ".Services");
    }
    public static IServiceCollection AddInfrastructureServiceFromAssembly(this IServiceCollection services)
    {
        return services.AddImplementationsFromAssembly(typeof(InfrastructureAssemblyMarker).Assembly, "Repository", ".Repositories");
    }
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher, PasswordHashder>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
    public static IServiceCollection InitCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddCorsPolicy(configuration);

        services.AddPersistenceServices();
        services.AddApplicationServicesFromAssembly();

        services.AddInfrastructureServiceFromAssembly();

        return services;
    }
}

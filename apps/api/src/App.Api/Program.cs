using App.Api.Data.Seeders;
using App.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
app.UseMiddleware<App.Api.Middlewares.ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Lifetime.ApplicationStarted.Register(() =>
{
    ILogger logger = app.Logger;

    foreach (var url in app.Urls)
    {
        logger.LogInformation("Listening on: {Url}", url);
        logger.LogInformation("OpenAPI: {Url}/openapi/v1.json", url);
        logger.LogInformation("Scalar : {Url}/scalar/v1", url);
    }
});


app.Run();


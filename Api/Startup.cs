using System.ComponentModel;
using System.Text.Json.Serialization;
using Api.Handlers;
using LinkModule.Infrastructure;
using LinkModule.Persistence.Contexts;
using Shared.Domain.Settings;
using Shared.Persistence;

namespace Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var configurationKey = $"{nameof(DbConnectionSettings)}:{nameof(LinkModuleDatabaseContext)}";
        var dbConnectionSettings = configuration.GetSection(configurationKey).Get<DbConnectionSettings>();
        services.AddKeyedSingleton(nameof(LinkModuleDatabaseContext), dbConnectionSettings);
        services.RegisterDbContext<LinkModuleDatabaseContext>(dbConnectionSettings);

        services.AddLinkModuleInfrastructure();

        var assembly = typeof(LinkModule.Api.Controllers.LinkController).Assembly;
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            })
            .AddApplicationPart(assembly);

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapControllers();
        app.MapFallbackToController("Index", "Redirect");

        app.UseExceptionHandler();

        app.ConfigureDbContext<LinkModuleDatabaseContext>();
    }
}
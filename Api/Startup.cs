using System.Reflection;
using System.Text.Json.Serialization;
using AnalyticModule.Api.Consumers;
using AnalyticModule.Infrastructure;
using AnalyticModule.Persistence.Contexts;
using Api.Handlers;
using LinkModule.Infrastructure;
using LinkModule.Persistence.Contexts;
using MassTransit;
using Shared.Domain.Settings;
using Shared.Persistence;

namespace Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var apiAssemblies = new List<Assembly>();

        #region Link Module

        var linkModuleDbConnectionSettings = configuration
            .GetSection($"{nameof(DbConnectionSettings)}:{nameof(LinkModuleDatabaseContext)}")
            .Get<DbConnectionSettings>();
        services.AddKeyedSingleton(nameof(LinkModuleDatabaseContext), linkModuleDbConnectionSettings);
        services.RegisterDbContext<LinkModuleDatabaseContext>(linkModuleDbConnectionSettings);

        services.AddLinkModuleInfrastructure();
        apiAssemblies.Add(typeof(LinkModule.Api.Controllers.LinkController).Assembly);

        #endregion

        #region Analytic Module

        var analyticModuleDbConnectionSettings = configuration
            .GetSection($"{nameof(DbConnectionSettings)}:{nameof(AnalyticModuleDatabaseContext)}")
            .Get<DbConnectionSettings>();
        services.AddKeyedSingleton(nameof(AnalyticModuleDatabaseContext), analyticModuleDbConnectionSettings);
        services.RegisterDbContext<AnalyticModuleDatabaseContext>(analyticModuleDbConnectionSettings);

        services.AddAnalyticModuleInfrastructure();

        #endregion

        var builder = services.AddControllers();

        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = false;
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        });
        foreach (var assembly in apiAssemblies)
        {
            builder.AddApplicationPart(assembly);
        }

        var rabbitMqSettings = configuration
            .GetSection(nameof(RabbitMqSettings))
            .Get<RabbitMqSettings>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateClickEventMessageConsumer>();

            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost, h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

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
        app.ConfigureDbContext<AnalyticModuleDatabaseContext>();
    }
}
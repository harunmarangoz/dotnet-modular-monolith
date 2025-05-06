using System.Reflection;
using System.Text.Json.Serialization;
using AnalyticModule.Api;
using AnalyticModule.Api.Consumers;
using AnalyticModule.Persistence.Contexts;
using Api.Handlers;
using LinkModule.Api;
using LinkModule.Persistence.Contexts;
using MassTransit;
using Microsoft.OpenApi.Models;
using QrModule.Api;
using Shared.Api;
using Shared.Domain.Settings;
using Shared.Persistence.Extensions;

namespace Api;

public class Startup
{
    private readonly List<IModuleStartup> _moduleStartups =
    [
        new AnalyticModuleStartup(),
        new LinkModuleStartup(),
        new QrModuleStartup()
    ];

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var apiAssemblies = new List<Assembly>()
        {
            typeof(AnalyticModule.Api.Controllers.AnalyticController).Assembly,
            typeof(LinkModule.Api.Controllers.LinkController).Assembly,
            typeof(QrModule.Api.Controllers.QrController).Assembly
        };

        foreach (var moduleStartup in _moduleStartups)
        {
            moduleStartup.ConfigureServices(services, configuration);
        }

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

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Modular Monolith",
                Version = "v1"
            });
        });

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

        var redisSettings = configuration
            .GetSection($"{nameof(RedisSettings)}")
            .Get<RedisSettings>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSettings.Configuration;
            options.InstanceName = redisSettings.InstanceName;
        });
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        foreach (var moduleStartup in _moduleStartups)
        {
            moduleStartup.Configure(app, env);
        }

        app.UseRouting();

        app.MapControllers();
        app.MapFallbackToController("Index", "Redirect");

        app.UseExceptionHandler();
    }
}
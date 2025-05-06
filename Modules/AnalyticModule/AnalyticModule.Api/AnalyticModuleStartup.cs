using AnalyticModule.Infrastructure;
using AnalyticModule.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Api;
using Shared.Domain.Settings;
using Shared.Persistence.Extensions;

namespace AnalyticModule.Api;

public class AnalyticModuleStartup : IModuleStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var analyticModuleDbConnectionSettings = configuration
            .GetSection($"{nameof(DbConnectionSettings)}:{nameof(AnalyticModuleDatabaseContext)}")
            .Get<DbConnectionSettings>();
        services.AddKeyedSingleton(nameof(AnalyticModuleDatabaseContext), analyticModuleDbConnectionSettings);
        services.RegisterDbContext<AnalyticModuleDatabaseContext>(analyticModuleDbConnectionSettings);

        services.AddAnalyticModuleInfrastructure();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.ConfigureDbContext<AnalyticModuleDatabaseContext>();
    }
}
using LinkModule.Infrastructure;
using LinkModule.Infrastructure.Services;
using LinkModule.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Api;
using Shared.Application.Services;
using Shared.Domain.Settings;
using Shared.Persistence.Extensions;

namespace LinkModule.Api;

public class LinkModuleStartup : IModuleStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var linkModuleDbConnectionSettings = configuration
            .GetSection($"{nameof(DbConnectionSettings)}:{nameof(LinkModuleDatabaseContext)}")
            .Get<DbConnectionSettings>();
        services.AddKeyedSingleton(nameof(LinkModuleDatabaseContext), linkModuleDbConnectionSettings);
        services.RegisterDbContext<LinkModuleDatabaseContext>(linkModuleDbConnectionSettings);

        services.AddLinkModuleInfrastructure();
        services.AddScoped<ILinkModuleService, LinkModuleService>();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.ConfigureDbContext<LinkModuleDatabaseContext>();
    }
}
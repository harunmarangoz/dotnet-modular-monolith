using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QrModule.Infrastructure;
using QrModule.Infrastructure.Services;
using Shared.Api;
using Shared.Application.Services;

namespace QrModule.Api;

public class QrModuleStartup : IModuleStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddQrModuleInfrastructure();
        services.AddScoped<IQrModuleService, QrModuleService>();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
    }
}
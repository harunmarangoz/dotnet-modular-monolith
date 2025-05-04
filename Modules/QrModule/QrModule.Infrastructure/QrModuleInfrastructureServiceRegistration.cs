using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace QrModule.Infrastructure;

public static class QrModuleInfrastructureServiceRegistration
{
    public static void AddQrModuleInfrastructure(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }
}
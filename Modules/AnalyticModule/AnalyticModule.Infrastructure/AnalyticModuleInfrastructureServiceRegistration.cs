using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticModule.Infrastructure;

public static class AnalyticModuleInfrastructureServiceRegistration
{
    public static void AddAnalyticModuleInfrastructure(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }
}
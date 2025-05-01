using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LinkModule.Infrastructure;

public static class LinkModuleInfrastructureServiceRegistration
{
    public static void AddLinkModuleInfrastructure(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }
}
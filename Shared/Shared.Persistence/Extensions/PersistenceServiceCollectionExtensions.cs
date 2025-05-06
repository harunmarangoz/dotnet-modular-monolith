using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Settings;

namespace Shared.Persistence.Extensions;

public static class PersistenceServiceCollectionExtensions
{
    public static void RegisterDbContext<TContext>(this IServiceCollection services, DbConnectionSettings settings)
        where TContext : DbContext
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<TContext>((sp, options) =>
        {
            options.UseNpgsql(settings.ConnectionString, optionsBuilder =>
            {
                optionsBuilder.CommandTimeout(15);
                optionsBuilder.MigrationsHistoryTable($"public.EFCore_MigrationHistory_{settings.Scheme}");
            });
        });
        services.AddDbContextFactory<TContext>(lifetime: ServiceLifetime.Scoped);
    }

    public static void ConfigureDbContext<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.Migrate();
    }
}
using AnalyticModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Settings;

namespace AnalyticModule.Persistence.Contexts;

public class AnalyticModuleDatabaseContext : DbContext
{
    private readonly DbConnectionSettings _dbConnectionSettings;

    public AnalyticModuleDatabaseContext(
        [FromKeyedServices(nameof(AnalyticModuleDatabaseContext))]
        DbConnectionSettings dbConnectionSettings,
        DbContextOptions<AnalyticModuleDatabaseContext> options)
        : base(options)
    {
        _dbConnectionSettings = dbConnectionSettings;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_dbConnectionSettings.Scheme);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ClickEvent> ClickEvents { get; set; }
}
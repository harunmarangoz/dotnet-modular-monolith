using LinkModule.Domain.Entities;
using LinkModule.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Settings;

namespace LinkModule.Persistence.Contexts;

public class LinkModuleDatabaseContext : DbContext
{
    private readonly DbConnectionSettings _dbConnectionSettings;

    public LinkModuleDatabaseContext(
        [FromKeyedServices(nameof(LinkModuleDatabaseContext))]
        DbConnectionSettings dbConnectionSettings,
        DbContextOptions<LinkModuleDatabaseContext> options)
        : base(options)
    {
        _dbConnectionSettings = dbConnectionSettings;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_dbConnectionSettings.Scheme);

        modelBuilder.ApplyConfiguration(new LinkConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Link> Links { get; set; }
}
using LinkModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkModule.Persistence.Configurations;

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.Property(x => x.UniqueKey).IsRequired();
        builder.HasIndex(x => x.UniqueKey).IsUnique();

        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionSa.Core.Entities;

namespace OnionSa.Infrastructure.Persistence.Configurations;

public class ClientConfigurations : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .HasKey(c => c.Document);

        builder
            .HasMany(c => c.Orders)
            .WithOne();
    }
}

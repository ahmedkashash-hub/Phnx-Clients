
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phnx.Domain.Entities;

namespace Airport.Infrastructure.Persistence.Configurations;

public class AdvertisementsConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Xmin)
            .IsRowVersion()
            .HasColumnName("xmin")
            .IsConcurrencyToken();
    }
}
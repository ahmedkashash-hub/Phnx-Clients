
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phnx.Domain.Entities;

namespace Airport.Infrastructure.Persistence.Configurations;

public class EventsConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Xmin)
            .IsRowVersion()
            .HasColumnName("xmin")
            .IsConcurrencyToken();
    }
}

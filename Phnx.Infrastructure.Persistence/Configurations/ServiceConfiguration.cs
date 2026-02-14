using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phnx.Domain.Entities;

namespace Phnx.Infrastructure.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            // Keep persistence compatible with existing migration column names.
            builder.Property(x => x.IpAddress).HasColumnName("Category");
            builder.Property(x => x.Provider).HasColumnName("BaseRate");

            builder.Property(x => x.Xmin)
                .IsRowVersion()
                .HasColumnName("xmin")
                .IsConcurrencyToken();
        }
    }
}

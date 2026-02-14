using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phnx.Domain.Entities;

namespace Phnx.Infrastructure.Persistence.Configurations
{
    public class AgencyTaskConfiguration : IEntityTypeConfiguration<AgencyTask>
    {
        public void Configure(EntityTypeBuilder<AgencyTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Xmin)
                .IsRowVersion()
                .HasColumnName("xmin")
                .IsConcurrencyToken();
        }
    }
}

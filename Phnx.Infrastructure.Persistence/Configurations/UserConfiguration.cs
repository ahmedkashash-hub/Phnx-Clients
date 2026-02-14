
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phnx.Domain.Entities;

namespace Phnx.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Xmin)
            .IsRowVersion()
            .HasColumnName("xmin")
            .IsConcurrencyToken();
        builder.HasIndex(x => x.Email);
        builder.HasData(User.Seed());
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phnx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Infrastructure.Persistence.Configurations
{

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Xmin)
                .IsRowVersion()
                .HasColumnName("xmin")
                .IsConcurrencyToken();

        }
    }
}

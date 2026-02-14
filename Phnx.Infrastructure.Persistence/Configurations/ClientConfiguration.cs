using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phnx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Infrastructure.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Xmin)
                .IsRowVersion()
                .HasColumnName("xmin")
                .IsConcurrencyToken();
            builder.HasIndex(x => x.Email);
        }
   
    }
}

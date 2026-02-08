using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Phnx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Infrastructure.Persistence.Configurations
{

        public class ProjectConfiguration : IEntityTypeConfiguration<Project>
        {
            public void Configure(EntityTypeBuilder<Project> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Xmin)
                    .IsRowVersion()
                    .HasColumnName("xmin")
                    .IsConcurrencyToken();
            }
        }

    }


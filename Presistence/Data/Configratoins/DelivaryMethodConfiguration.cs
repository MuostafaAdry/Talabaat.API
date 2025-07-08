using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configratoins
{
     class DelivaryMethodConfiguration : IEntityTypeConfiguration<DelivaryMethod>
    {
        public void Configure(EntityTypeBuilder<DelivaryMethod> builder)
        {
            builder.ToTable("DelivaryMethods");

            builder.Property(e => e.Price)
                .HasColumnType("decimal(8,2)");

            builder.Property(e => e.ShortName)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(e => e.Description)
               .HasColumnType("varchar")
               .HasMaxLength(100);

            builder.Property(e => e.DelivaryTime)
               .HasColumnType("varchar")
               .HasMaxLength(50);
        }
    }
}

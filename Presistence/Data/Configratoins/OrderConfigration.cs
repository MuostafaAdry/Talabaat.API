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
    class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(e => e.SubTotal)
                .HasColumnType("decimal(8,2)");

            builder.HasMany(e => e.Items)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.DelivaryMethod)
                .WithMany()
                .HasForeignKey(e => e.DelivaryMethodId);

            builder.OwnsOne(e => e.ShipToAddress);
        }
    }
}

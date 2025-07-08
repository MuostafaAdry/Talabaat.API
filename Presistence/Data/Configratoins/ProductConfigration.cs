using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configratoins
{
    class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(e => e.ProductBrand)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.ProductBrandId);

            builder.HasOne(e => e.ProductType)
              .WithMany(e => e.Products)
              .HasForeignKey(e => e.ProductTypeId);
            builder.Property(e => e.Price)
                .HasColumnType("decimal(10,3)");
        }
    }
}


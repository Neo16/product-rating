using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRating.Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Dal.Configure
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder
                .HasDiscriminator<string>("Type");

            builder
              .HasOne(e => e.Product)
              .WithMany(e => e.PropertyValues)
              .HasForeignKey(e => e.ProductId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRating.Model.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Dal.Configure
{
    public class CategoryProductPropertyTypeConfiguration : IEntityTypeConfiguration<CategoryProductProperty>
    {
        public void Configure(EntityTypeBuilder<CategoryProductProperty> builder)
        {
            builder.HasOne(e => e.Category)
               .WithMany(e => e.PropertyTypes)
               .HasForeignKey(e => e.CategoryId);

            builder.HasOne(e => e.ProductPropertyType)
              .WithMany()
              .HasForeignKey(e => e.ProductPropertyTypeId);
        }
    }
}

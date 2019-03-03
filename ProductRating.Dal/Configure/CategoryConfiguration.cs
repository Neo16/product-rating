using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRating.Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Dal.Configure
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(e => e.Children)
                 .WithOne(e => e.Parent)
                 .HasForeignKey(e => e.ParentId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(e => e.Attributes)
               .WithOne(e => e.Category)
               .HasForeignKey(e => e.CategoryId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRating.Dal.Model.Entities.Products;

namespace ProductRating.Dal.Configure
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder
                .HasDiscriminator<string>("Type");

            builder
              .HasMany(e => e.ProductConnctions)
              .WithOne(e => e.ProductAttributeValue)
              .HasForeignKey(e => e.ProductAttributeValueId);
        }
    }
}

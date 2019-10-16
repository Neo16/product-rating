using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRating.Dal.Model.Entities.Products;

namespace ProductRating.Dal.Configure
{
    public class ProductAttributeValueConnectionConfiguration : IEntityTypeConfiguration<ProductAttributeValueConnection>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValueConnection> builder)
        {
            builder.HasKey(e => new { e.ProductAttributeValueId, e.ProductId });           
        }
    }
}

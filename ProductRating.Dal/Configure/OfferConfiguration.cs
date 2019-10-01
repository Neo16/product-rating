using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRating.Model.Entities.Products;

namespace ProductRating.Dal.Configure
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => new { o.SellerId, o.ProductId });
            builder.HasOne(e => e.Seller)
               .WithMany(e => e.Sells)
               .HasForeignKey(e => e.SellerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Product)
              .WithMany(e => e.Offers)
              .HasForeignKey(e => e.ProductId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

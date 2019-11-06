using System;
using System.ComponentModel.DataAnnotations;

namespace ProductRating.Dal.Model.Entities.Products
{
    public class ProductPicture
    {
        public Product Product { get; set; }

        [Key]
        public Guid ProductId { get; set; }

        public Picture Picture { get; set; }

        [Key]
        public Guid PictureId { get; set; }
    }
}

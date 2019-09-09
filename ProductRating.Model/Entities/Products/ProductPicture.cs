using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductRating.Model.Entities.Products
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

using ProductRating.Dal.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Dal.Model.Entities.Products
{
    public class Brand : IEntity
    {        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public Guid? CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ProductRating.Dal.Model.Entities;
using ProductRating.Dal.Model.Entities.Products;
using ProductRating.Dal.Model.Entities.Reviews;

namespace ProductRating.Dal.Model.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string NickName { get; set; }

        public Picture Avatar { get; set; }

        public Guid? AvatarId { get; set; }

        public string Nationality { get; set; }

        public string Introduction { get; set; }

        public string Address { get; set; }      

        public ICollection<TextReview> TextReviews { get; set; }

        public ICollection<Product> CreatedProducts { get; set; }

        public ICollection<Offer> Sells { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Products
{
    public class Picture : IEntity
    {
        public Guid Id { get; set; }

        public string Url { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Products
{
    public class Brand : IEntity
    {        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
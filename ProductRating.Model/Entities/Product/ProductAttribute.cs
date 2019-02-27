﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Product
{
    public class ProductAttribute : IEntity
    {
        public Guid Id { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public string Name  { get; set; }
    }
}
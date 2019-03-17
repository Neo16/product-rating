using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities
{
    public class Picture : IEntity
    {
        public Guid Id { get; set; }

        public byte[] Data { get; set; }
    }
}

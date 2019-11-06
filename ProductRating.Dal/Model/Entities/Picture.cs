using System;

namespace ProductRating.Dal.Model.Entities
{
    public class Picture : IEntity
    {
        public Guid Id { get; set; }

        public byte[] Data { get; set; }
    }
}

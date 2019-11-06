using System;

namespace ProductRating.Dal.Model.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}

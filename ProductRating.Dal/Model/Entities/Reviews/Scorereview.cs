using System;

namespace ProductRating.Dal.Model.Entities.Reviews
{
    public class Scorereview : ReviewBase, IEntity
    {
        public Guid Id { get; set; }

        public int Value { get; set; }
    }
}

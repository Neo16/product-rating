using System;

namespace ProductRating.Model.Entities.Reviews
{
    public class Scorereview : ReviewBase, IEntity
    {
        public Guid Id { get; set; }       

        public int Value { get; set; }      
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities
{
    public interface IEntity
    {
        Guid Id { get;  set; }
    }
}

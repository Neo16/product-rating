using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Dal.Model.Entities
{
    public interface IEntity
    {
        Guid Id { get;  set; }
    }
}

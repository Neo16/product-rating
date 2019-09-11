using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Brand
{
    public class ManageBrandFilterDto
    {
        public string Name { get; set; }
        public bool? IsMine { get; set; }
    }
}

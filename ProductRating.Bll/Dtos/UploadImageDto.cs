using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos
{
    public class UploadImageDto
    {
        public IFormFile file { get; set; }
    }
}

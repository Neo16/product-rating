using Microsoft.AspNetCore.Http;

namespace ProductRating.Bll.Dtos
{
    public class UploadImageDto
    {
        public IFormFile file { get; set; }
    }
}

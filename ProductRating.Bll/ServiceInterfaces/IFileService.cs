using Microsoft.AspNetCore.Http;
using ProductRating.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IFileService
    {
        Task<PictureDto> UploadPicture(IFormFile file);
    }
}

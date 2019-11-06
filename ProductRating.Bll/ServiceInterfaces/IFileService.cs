using Microsoft.AspNetCore.Http;
using ProductRating.Bll.Dtos;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IFileService
    {
        Task<PictureDto> UploadPicture(IFormFile file);
    }
}

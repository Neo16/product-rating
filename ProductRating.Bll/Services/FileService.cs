using Microsoft.AspNetCore.Http;
using ProductRating.Bll.Exceptions;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class FileService : ServiceBase, IFileService
    {
        public FileService(ApplicationDbContext ctx) : base(ctx)
        {

        }

        public async Task<Guid> UploadPicture(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var picture = new Picture() { Data = fileBytes };

                    context.Pictures.Add(picture);
                    await context.SaveChangesAsync();

                    return picture.Id;
                }
            }

            throw new BusinessLogicException("Can not upload picture: File is empty")
            {
                ErrorCode = ErrorCode.InvalidArgument
            };
        }
    }
}

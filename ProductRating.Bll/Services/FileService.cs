using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Exceptions;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Dal.Model.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class FileService : ServiceBase, IFileService
    {
        private readonly IMapper mapper;

        public FileService(ApplicationDbContext ctx, IMapper mapper) : base(ctx)
        {
            this.mapper = mapper;
        }

        public async Task<PictureDto> UploadPicture(IFormFile file)
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

                    return mapper.Map<PictureDto>(picture);
                }
            }

            throw new BusinessLogicException("Can not upload picture: File is empty")
            {
                ErrorCode = ErrorCode.InvalidArgument
            };
        }
    }
}

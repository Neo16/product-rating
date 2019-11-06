using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.ServiceInterfaces;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [ApiController]
    [Route("files")]
    public class FilesController : Controller
    {
        private readonly IFileService fileService;

        public FilesController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpPost]
        [Route("upload-picture")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(PictureDto), 200)]
        public async Task<IActionResult> UploadPicture([FromForm] UploadImageDto uploadImageDto)
        {
            if (uploadImageDto?.file == null)
            {
                return BadRequest();
            }

            PictureDto uploadedPicture = await fileService.UploadPicture(uploadImageDto.file);
            return Ok(uploadedPicture);
        }
    }
}

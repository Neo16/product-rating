using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.ServiceInterfaces;
using System.Net;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> UploadPlannyPicture([FromForm] IFormFile Picture)
        {
            if (Picture == null)
            {
                return BadRequest();
            }

            var uploadedPictureId = await fileService.UploadPicture(Picture);
            return Ok(uploadedPictureId);
        }
    }
}

using Application.Interfaces;
using Application.Models.Requests.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }


        [HttpPost]
        public IActionResult UploadImage([FromBody] UploadImageDTO imageDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var upload = _imageService.UploadImage(imageDTO, userId);
                if(upload == true)
                {
                    return Ok();
                }
                return Unauthorized();
            }
          

        [HttpPut("{id}")]
        public IActionResult UpdateImage(int id, [FromBody]UpdateImageDTO imageDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var updated = _imageService.UpdateImage(id, imageDTO, userId);
            if(updated == true)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var deleted = _imageService.DeleteImage(id, userId);
            if (deleted == true)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}

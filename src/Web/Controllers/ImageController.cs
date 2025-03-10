using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("[action]")]
        public IActionResult UploadImage([FromBody] UploadImageDTO imageDTO)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var upload = _imageService.UploadImage(imageDTO, userId);
                if(upload == true)
                {
                    return Ok();
                }
                return Unauthorized();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]/{id}")]
        public IActionResult UpdateImage(int id, [FromBody]UpdateImageDTO imageDTO)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var updated = _imageService.UpdateImage(id, imageDTO, userId);
                if(updated == true)
                {
                    return Ok();
                }else
                {
                    return BadRequest();
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteImage(int id)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
               var deleted = _imageService.DeleteImage(id, userId);
                if(deleted == true)
                {
                    return Ok();
                }
                return BadRequest();
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }



    }
}

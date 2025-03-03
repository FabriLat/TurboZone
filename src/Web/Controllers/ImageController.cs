using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                var upload = _imageService.UploadImage(imageDTO);
                if(upload == true)
                {
                    return Ok();
                }
                return BadRequest("Limite de imagenes alcanzado");
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
               var updated = _imageService.UpdateImage(id, imageDTO);
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



    }
}

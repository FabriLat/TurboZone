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


        /// <summary>
        /// Sube una nueva imagen.
        /// </summary>
        /// <param name="imageDTO">Objeto con los datos necesarios para subir la imagen.</param>
        /// <returns>Respuesta exitosa si la imagen se sube correctamente.</returns>
        /// <response code="200">Imagen subida correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint permite a los usuarios autenticados subir una nueva imagen.
        /// </remarks>
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


        /// <summary>
        /// Actualiza los datos de una imagen existente.
        /// </summary>
        /// <param name="id">ID de la imagen a actualizar.</param>
        /// <param name="imageDTO">Nuevo objeto con los datos de la imagen.</param>
        /// <returns>Respuesta exitosa si la imagen se actualiza correctamente.</returns>
        /// <response code="200">Imagen actualizada correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <response code="404">Imagen no encontrada.</response>
        /// <remarks>
        /// Este endpoint permite actualizar los datos de una imagen. El usuario debe estar autenticado.
        /// </remarks>
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


        /// <summary>
        /// Elimina una imagen.
        /// </summary>
        /// <param name="id">ID de la imagen a eliminar.</param>
        /// <returns>Respuesta exitosa si la imagen se elimina correctamente.</returns>
        /// <response code="200">Imagen eliminada correctamente.</response>
        /// <response code="400">No se pudo eliminar la imagen.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint permite a los usuarios autenticados eliminar una imagen.
        /// </remarks>
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

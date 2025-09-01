using Application.Interfaces;
using Application.Models.Requests.Comments;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        /// <summary>
        /// Crea un nuevo comentario.
        /// </summary>
        /// <param name="commentDTO">Objeto con los datos del comentario a crear.</param>
        /// <returns>El comentario creado si la operación es exitosa.</returns>
        /// <response code="201">Comentario creado correctamente.</response>
        /// <response code="400">Datos inválidos o no se pudo crear el comentario.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Solo los usuarios autenticados pueden crear comentarios.
        /// </remarks>
        [HttpPost]
        public IActionResult Create([FromBody] CreateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "Usuario no identificado" });
            }

            int userId = int.Parse(userIdClaim.Value);
            var createdComment = _commentService.AddComment(userId, commentDTO);
            if (createdComment == null)
            {
                return BadRequest(new { Message = "No se pudo crear el comentario" });
            }
            return CreatedAtAction(nameof(Get), new { id = createdComment.Id }, createdComment);
        }



        /// <summary>
        /// Obtiene los detalles de un comentario específico.
        /// </summary>
        /// <param name="id">ID del comentario a obtener.</param>
        /// <returns>Comentario encontrado si la operación es exitosa.</returns>
        /// <response code="200">Comentario encontrado y devuelto correctamente.</response>
        /// <response code="404">Comentario no encontrado.</response>
        /// <remarks>
        /// Este endpoint permite obtener los detalles de un comentario específico.
        /// </remarks>
        [HttpGet("{id}")]
        public ActionResult<Comment> Get(int id)
        {
            Comment? comment = _commentService.GetComment(id);
            if(comment == null)
            {
                return NotFound(new {Message = "Comentario no encontrado" });
            }
            return Ok(comment);
        }



        /// <summary>
        /// Obtiene todos los comentarios asociados a un vehículo.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo para obtener los comentarios asociados.</param>
        /// <returns>Lista de comentarios asociados al vehículo.</returns>
        /// <response code="200">Comentarios obtenidos correctamente.</response>
        /// <response code="404">No se encontraron comentarios para este vehículo.</response>
        /// <remarks>
        /// Este endpoint permite obtener todos los comentarios asociados a un vehículo específico.
        /// </remarks>
        [HttpGet("vehicle/{vehicleId}")]
        public ActionResult<List<Comment>> GetByVehicleId(int vehicleId)
        {
            var comments = _commentService.GetCommentsByVehicleId(vehicleId);
            if (comments == null || !comments.Any())
            {
                return NotFound(new { Message = "No se encontraron comentarios para este vehículo" });
            }
            return Ok(comments);
        }


        /// <summary>
        /// Actualiza un comentario existente.
        /// </summary>
        /// <param name="id">ID del comentario a actualizar.</param>
        /// <param name="comment">Nuevo contenido del comentario.</param>
        /// <returns>Sin contenido si la actualización fue exitosa.</returns>
        /// <response code="204">Comentario actualizado correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <response code="404">Comentario no encontrado o no autorizado.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Solo el propietario del comentario puede actualizarlo.
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody]string comment )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "Usuario no identificado" });
            }
            int userId = int.Parse(userIdClaim.Value);
            var updated = _commentService.UpdateComment(id, comment, userId );
            if (updated == null)
            {
                return BadRequest(); 
            }
            return NoContent();
        }


        /// <summary>
        /// Elimina un comentario.
        /// </summary>
        /// <param name="id">ID del comentario a eliminar.</param>
        /// <returns>Sin contenido si la eliminación fue exitosa.</returns>
        /// <response code="204">Comentario eliminado correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <response code="403">No tienes permisos para eliminar este comentario.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Solo el propietario del comentario o un administrador puede eliminarlo.
        /// </remarks>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "Usuario no identificado" });
            }

            int userId = int.Parse(userIdClaim.Value);
            var deleted = _commentService.DeleteComment(userId, id);
            if (!deleted)
            {
                return Forbid();
            }
            return NoContent();
        }
    }
}
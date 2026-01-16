using Application.Interfaces;
using Application.Models.Requests.Comments;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/vehicles/{vehicleId}/comments")]
    [ApiController]
    public class VehicleCommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public VehicleCommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Obtiene todos los comentarios de un vehículo.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Comment>> GetAll(int vehicleId)
        {
            var comments = _commentService.GetCommentsByVehicleId(vehicleId);

            if (comments == null || !comments.Any())
                return NotFound(new { Message = "No se encontraron comentarios para este vehículo" });

            return Ok(comments);
        }

        /// <summary>
        /// Obtiene un comentario específico de un vehículo.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Comment> GetById(int vehicleId, int id)
        {
            var comment = _commentService.GetComment(id);

            if (comment == null || comment.VehicleId != vehicleId)
                return NotFound(new { Message = "Comentario no encontrado para este vehículo" });

            return Ok(comment);
        }

        /// <summary>
        /// Crea un nuevo comentario para un vehículo.
        /// </summary>
        [HttpPost]
        [Authorize]
        public IActionResult Create(int vehicleId, [FromBody] CreateCommentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
                
            var created = _commentService.AddComment(userId, vehicleId, dto);

            if (created == null)
                return BadRequest(new { Message = "No se pudo crear el comentario" });

            return CreatedAtAction(
                nameof(GetById),
                new { vehicleId, id = created.Id },
                created
            );
        }

        /// <summary>
        /// Actualiza un comentario existente.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int vehicleId, int id, [FromBody] string comment)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var updated = _commentService.UpdateComment(id, comment, userId);

            if (updated == null || updated.VehicleId != vehicleId)
                return NotFound(new { Message = "Comentario no encontrado o sin permisos" });

            return NoContent();
        }

        /// <summary>
        /// Elimina un comentario.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int vehicleId, int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var deleted = _commentService.DeleteComment(userId, id);

            if (!deleted)
                return Forbid();

            return NoContent();
        }
    }
}

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
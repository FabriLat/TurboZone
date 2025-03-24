using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("[action]")]
        public IActionResult AddComment([FromBody] CreateCommentDTO commentDTO)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            _commentService.AddComment(userId, commentDTO);
            return Ok();
        }

        [HttpGet("[action]/{vehicleId}")]
        public List<Comment> GetCommentsByVehicleId([FromRoute]int vehicleId)
        {
            try
            {
                return _commentService.GetCommentsByVehicleId(vehicleId);
            }catch (Exception ex) 
            {
                return new List<Comment>();
            }
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var response = _commentService.DeleteComment(userId, id);
                if(response == true)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


    }
}

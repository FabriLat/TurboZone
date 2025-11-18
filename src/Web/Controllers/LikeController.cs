using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        
        [HttpPost("{vehicleId}")]
        [Authorize]
        public ActionResult LikeVihicle(int vehicleId)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            bool result = _likeService.LikeVehicle(vehicleId, currentUserId);

            if (result)
            {
                return Ok("Vehicle liked successfully.");
            }
            else
            {
                return BadRequest("Unable to like the vehicle.");
            }
        }

        [HttpDelete("{vehicleId}")]
        [Authorize]
        public ActionResult DeleteVehicleLike([FromRoute] int vehicleId)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            bool result = _likeService.UnlikeVehicle(vehicleId, currentUserId);
            if (result)
            {
                return Ok("Like removed successfully.");
            }
            else
            {
                return NotFound("Like not found.");
            }
        }



    }
}

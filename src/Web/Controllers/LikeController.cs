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


        /// <summary>
        /// Agrega un like a un vehículo.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo al que se le dará like.</param>
        /// <returns>Resultado de la operación.</returns>
        /// <response code="200">Like agregado correctamente.</response>
        /// <response code="400">No se pudo agregar el like.</response>
        /// <response code="401">No autenticado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint permite a un usuario autenticado dar like a un vehículo.
        /// </remarks>
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

        /// <summary>
        /// Elimina el like de un vehículo.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo al que se le quitará el like.</param>
        /// <returns>Resultado de la operación.</returns>
        /// <response code="200">Like eliminado correctamente.</response>
        /// <response code="404">No se encontró el like.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint permite a un usuario autenticado quitar el like de un vehículo.
        /// </remarks>
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

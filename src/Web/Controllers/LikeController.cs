using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/vehicles/{vehicleId}/likes")]
    [ApiController]
    public class VehicleLikesController : ControllerBase
    {
        private readonly IVehicleLikeService _likeService;
        public VehicleLikesController(IVehicleLikeService likeService)
        {
            _likeService = likeService;
        }


        /// <summary>
        /// Verifica si el vehículo fue likeado por el usuario autenticado.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo a verificar.</param>
        /// <returns>
        /// <c>true</c> si el usuario autenticado dio like al vehículo; de lo contrario, <c>false</c>.
        /// </returns>
        /// <response code="200">Resultado de la verificación.</response>
        /// <response code="401">No autenticado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint devuelve false si el usuario no está autenticado.
        /// </remarks>
        [HttpGet("me")]
        public ActionResult<bool> LikedByUser(int vehicleId)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            return currentUserId != 0 && _likeService.IsVehicleLikedByUser(vehicleId, currentUserId);
        }

        /// <summary>
        /// Obtiene la cantidad total de likes de un vehículo.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo.</param>
        /// <returns>Cantidad total de likes.</returns>
        /// <response code="200">Likes obtenidos correctamente.</response>
        /// <remarks>
        /// Este endpoint es público y no requiere autenticación.
        /// </remarks>
        [HttpGet]
        public ActionResult<int> GetLikes(int vehicleId)
        {
            var likes = _likeService.GetTotalLikes(vehicleId);
            return Ok(likes);
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
        [HttpPost]
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
        [HttpDelete]
        [Authorize]
        public ActionResult DeleteVehicleLike(int vehicleId)
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

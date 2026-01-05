using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ViewController : ControllerBase
    {

        private readonly IViewService _viewService;
        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }

        /// <summary>
        /// Registra una visualización de un vehículo.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo visualizado.</param>
        /// <returns>Respuesta sin contenido.</returns>
        /// <response code="204">Visualización registrada correctamente.</response>
        /// <remarks>
        /// Este endpoint registra una vista del vehículo.
        /// Si el usuario no está autenticado, la vista se registra como anónima.
        /// </remarks>
        [HttpPost("{vehicleId}")]
        public ActionResult ViewVehicle(int vehicleId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _viewService.LogView(vehicleId, null);
                return NoContent();
            }
            _viewService.LogView(vehicleId, int.Parse(userId));
            return NoContent();
        }

        /// <summary>
        /// Obtiene el total de visualizaciones de un vehículo.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo.</param>
        /// <returns>Total de visualizaciones.</returns>
        /// <response code="200">Cantidad de visualizaciones obtenida correctamente.</response>
        /// <remarks>
        /// Este endpoint devuelve la cantidad total de vistas de un vehículo.
        /// </remarks>
        [HttpGet("{vehicleId}")]
        public int Get(int vehicleId)
        {
            int totalViews = _viewService.GetTotalViewsByVehicle(vehicleId);
            return totalViews;
        }
    }
}

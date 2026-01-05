using Application.Interfaces;
using Application.Models.Requests.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        private readonly ISpecificationService _specificationService;
        public SpecificationController(ISpecificationService specificationService)
        {
            _specificationService = specificationService;
        }



        /// <summary>
        /// Crea una nueva especificación para un vehículo.
        /// </summary>
        /// <param name="specificationDTO">Datos de la especificación.</param>
        /// <returns>Resultado de la operación.</returns>
        /// <response code="200">Especificación creada correctamente.</response>
        /// <response code="400">No se pudo crear la especificación.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint permite crear una especificación asociada a un vehículo existente.
        /// </remarks>
        [Authorize]
        [HttpPost]
        public IActionResult CreateSpecification(SpecificationDTO specificationDTO)
        {
            var result = _specificationService.AddSpecification(specificationDTO);
            if (!result)
            {
                return BadRequest("Could not create specification. Vehicle may not exist.");
            }
            return Ok("Specification created successfully.");
        }



        /// <summary>
        /// Obtiene las especificaciones de un vehículo por su ID.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo.</param>
        /// <returns>Especificación del vehículo.</returns>
        /// <response code="200">Especificación obtenida correctamente.</response>
        /// <response code="404">No se encontró la especificación.</response>
        /// <remarks>
        /// Este endpoint devuelve la especificación asociada a un vehículo.
        /// </remarks>
        [HttpGet("vehicle/{vehicleId}")]
        public IActionResult GetSpecificationByVehicleId(int vehicleId)
        {
            var specificationDTO = _specificationService.GetSpecificationByVehicleId(vehicleId);
            if (specificationDTO == null)
            {
                return NotFound("Specification not found for the given vehicle ID.");
            }
            return Ok(specificationDTO);
        }
    }
}

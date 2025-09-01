using Application.Interfaces;
using Application.Models.Requests.Vehicles;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        /// <summary>
        /// Crea un nuevo vehículo.
        /// </summary>
        /// <param name="createVehicle">Objeto con los datos necesarios para crear el vehículo.</param>
        /// <returns>El vehículo creado si la operación es exitosa.</returns>
        /// <response code="201">Vehículo creado correctamente.</response>
        /// <response code="400">Datos inválidos o no se pudo crear el vehículo.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. El usuario autenticado será el propietario del vehículo creado.
        /// </remarks>
        [HttpPost]
        [Authorize]
        public ActionResult CreateVehicle([FromBody] CreateVehicleDTO createVehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var create = _vehicleService.CreateVehicle(createVehicle, userId);
            if(create != null)
            {
                return CreatedAtAction(nameof(Get), new { id = create.Id }, create);
            }
            return BadRequest();  
        }


        /// <summary>
        /// Obtiene la lista de todos los vehículos.
        /// </summary>
        /// <returns>Lista de vehículos registrados.</returns>
        /// <response code="200">Lista de vehículos obtenida correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT y el usuario debe ser un moderador o SysAdmin para acceder.
        /// </remarks>
        [HttpGet]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetAll()
        {
            return _vehicleService.GetAllVehicles();
        }

        /// <summary>
        /// Obtiene los detalles de un vehículo específico por su ID.
        /// </summary>
        /// <param name="id">ID del vehículo a obtener.</param>
        /// <returns>Detalles del vehículo si se encuentra.</returns>
        /// <response code="200">Detalles del vehículo obtenidos correctamente.</response>
        /// <response code="404">Vehículo no encontrado.</response>
        /// <remarks>
        /// Este endpoint permite obtener la información detallada de un vehículo mediante su ID.
        /// </remarks>
        [HttpGet("{id}")]
        public VehicleDTO? Get([FromRoute]int id)
        {
                VehicleDTO? vehicleDto =  _vehicleService.GetVehicleById(id);
                if (vehicleDto != null)
                {
                    return vehicleDto;
                }
                return null;
        }


        /// <summary>
        /// Obtiene la lista de todos los vehículos activos.
        /// </summary>
        /// <returns>Lista de vehículos activos.</returns>
        /// <response code="200">Lista de vehículos activos obtenida correctamente.</response>
        /// <remarks>
        /// Este endpoint permite obtener solo los vehículos activos registrados.
        /// </remarks>
        [HttpGet("[action]")]
        public List<VehicleDTO> GetActive()
        {
            return _vehicleService.GetActiveVehicles();
        }


        /// <summary>
        /// Obtiene la lista de vehículos pendientes.
        /// </summary>
        /// <returns>Lista de vehículos pendientes.</returns>
        /// <response code="200">Lista de vehículos pendientes obtenida correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido con los permisos adecuados.</response>
        /// <remarks>
        /// Este endpoint requiere que el usuario sea un moderador o SysAdmin para acceder.
        /// </remarks>
        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetPending()
        {
            return _vehicleService.GetPendingVehicles();
        }


        /// <summary>
        /// Obtiene la lista de vehículos pendientes de actualización.
        /// </summary>
        /// <returns>Lista de vehículos pendientes de actualización.</returns>
        /// <response code="200">Lista de vehículos pendientes de actualización obtenida correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido con los permisos adecuados.</response>
        /// <remarks>
        /// Este endpoint requiere que el usuario sea un moderador o SysAdmin para acceder.
        /// </remarks>
        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetPendingUpdate()
        {
            
            return _vehicleService.GetPendingUpdateVehicles();
        }


        /// <summary>
        /// Actualiza los detalles de un vehículo.
        /// </summary>
        /// <param name="id">ID del vehículo a actualizar.</param>
        /// <param name="updateVehicle">Objeto con los datos actualizados del vehículo.</param>
        /// <returns>Sin contenido si la actualización fue exitosa.</returns>
        /// <response code="200">Vehículo actualizado correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint permite actualizar un vehículo. Solo el propietario del vehículo o un moderador puede realizar la actualización.
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult? Update(int id,[FromBody]UpdateVehicleDTO updateVehicle)
        {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var vehicle = _vehicleService.UpdateVehicle(updateVehicle, userId, id);
                if (vehicle != null)
                {
                    return Ok();
                }
                return Unauthorized();
        }


        /// <summary>
        /// Cambia el estado de un vehículo.
        /// </summary>
        /// <param name="id">ID del vehículo a actualizar.</param>
        /// <param name="newState">Nuevo estado del vehículo.</param>
        /// <returns>Respuesta exitosa si el estado fue actualizado correctamente.</returns>
        /// <response code="200">Estado del vehículo actualizado correctamente.</response>
        /// <response code="404">Vehículo no encontrado.</response>
        /// <remarks>
        /// Este endpoint requiere que el usuario sea un moderador o SysAdmin para cambiar el estado de un vehículo.
        /// </remarks>
        [HttpPut("changeState/{id}")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public ActionResult? ChangeState(int id, [FromBody]string newState)
        {
                if(_vehicleService.ChangeVehicleState(id, newState))
                {
                    return Ok();
                }
                return NotFound();
        }
    }
}
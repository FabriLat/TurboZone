using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Requests.Clients;
using Application.Models.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador para la gestión de clientes.
    /// Proporciona endpoints para crear, obtener, actualizar y eliminar clientes.
    /// </summary>
    [Route("api/[controller]s")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;

        public ClientController(IClientService clientService, IUserService userService)
        {
            _clientService = clientService;
            _userService = userService;
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="createClientDTO">Objeto con los datos requeridos para crear el cliente.</param>
        /// <returns>El cliente creado si la operación es exitosa.</returns>
        /// <response code="201">Cliente creado correctamente.</response>
        /// <response code="400">Datos inválidos o no se pudo crear el cliente.</response>
        /// <remarks>
        /// Este endpoint permite el registro de un nuevo cliente. Solo puede ser accedido por usuarios no autenticados.
        /// </remarks>
        [HttpPost]
        [Authorize(Policy = "AnonymousOnly")]
        public ActionResult Create([FromBody] CreateClientDTO createClientDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = _clientService.CreateNewClient(createClientDTO);
            if (created == null)
            {
                return BadRequest(new { Message = "No se pudo crear el cliente" });
            }
            return CreatedAtAction("Get","User" , new { id = created.Id }, created);
        }

        /// <summary>
        /// Obtiene la lista de todos los clientes.
        /// </summary>
        /// <returns>Lista de clientes registrados.</returns>
        /// <response code="200">Lista de clientes obtenida correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Devuelve todos los clientes registrados en el sistema.
        /// </remarks>
        [HttpGet]
        [Authorize]
        public ActionResult<List<ClientDTO>> GetAll()
        {
            var clients = _clientService.GetAllClients();
            return Ok(clients);
        }



        /// <summary>
        /// Elimina un cliente existente.
        /// </summary>
        /// <param name="id">ID del cliente a eliminar.</param>
        /// <returns>Sin contenido si la eliminación fue exitosa.</returns>
        /// <response code="204">Cliente eliminado correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <response code="403">Prohibido: solo el propio cliente puede eliminar su cuenta.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Solo el propio cliente puede eliminar su cuenta.
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "Usuario no identificado" });
            }

            int userId = int.Parse(userIdClaim.Value);
            var deletedClient = _clientService.DeleteClient(id, userId);
            if (deletedClient == null)
            {
                return Forbid();
            }

            return NoContent();
        }
    }
}
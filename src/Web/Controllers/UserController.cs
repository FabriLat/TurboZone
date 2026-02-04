using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Obtiene los detalles de un usuario específico.
        /// </summary>
        /// <param name="id">ID del usuario a obtener.</param>
        /// <returns>Detalles del usuario si se encuentra.</returns>
        /// <response code="200">Detalles del usuario obtenidos correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <remarks>
        /// Este endpoint permite a los usuarios autenticados obtener sus propios detalles o los detalles de otros usuarios.
        /// El acceso está restringido a usuarios autenticados con un token JWT válido.
        /// </remarks>
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { Message = "Usuario no encontrado" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="id">ID del cliente a actualizar.</param>
        /// <param name="updateUserDTO">Objeto con los datos actualizados del cliente.</param>
        /// <returns>Sin contenido si la actualización fue exitosa.</returns>
        /// <response code="204">Cliente actualizado correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <response code="404">Cliente no encontrado o no autorizado.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Solo el propio cliente puede actualizar sus datos.
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Update(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            bool updated = _userService.UpdateUser(userId, updateUserDTO, userId);
            if (!updated)
            {
                return NotFound(new { Message = "Cliente no encontrado o no autorizado" });
            }
            return NoContent();
        }



    }
}
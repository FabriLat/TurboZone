using Application.Interfaces;
using Application.Models.Requests.Moderators;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(Policy = "SysAdminOnly")]
    public class ModeratorController : ControllerBase
    {
        private readonly IModeratorService _moderatorService;
        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }


        /// <summary>
        /// Crea un nuevo moderador.
        /// </summary>
        /// <param name="createModerator">Objeto con los datos necesarios para crear el moderador.</param>
        /// <returns>El moderador creado si la operación es exitosa.</returns>
        /// <response code="201">Moderador creado correctamente.</response>
        /// <response code="400">Datos inválidos o no se pudo crear el moderador.</response>
        /// <remarks>
        /// Este endpoint requiere que el usuario sea un administrador del sistema (SysAdmin). Solo los SysAdmins pueden crear moderadores.
        /// </remarks>
        [HttpPost]
        public ActionResult CreateModerator([FromBody] CreateModeratorDTO createModerator)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var created = _moderatorService.CreateModerator(createModerator);
            if(created != null)
            {
                return CreatedAtAction("Get", "User", new { id = created.Id }, created);
            }
            return BadRequest();  
        }

        /// <summary>
        /// Obtiene la lista de todos los moderadores.
        /// </summary>
        /// <returns>Lista de moderadores registrados.</returns>
        /// <response code="200">Lista de moderadores obtenida correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Solo los moderadores y administradores del sistema pueden acceder a esta información.
        /// </remarks>
        [HttpGet]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<ModeratorDTO> GetAll()
        {
            return _moderatorService.GetAllModerators();
        }


        /// <summary>
        /// Actualiza los datos de un moderador existente.
        /// </summary>
        /// <param name="id">ID del moderador a actualizar.</param>
        /// <param name="updateModerator">Objeto con los nuevos datos del moderador.</param>
        /// <returns>Sin contenido si la actualización fue exitosa.</returns>
        /// <response code="204">Moderador actualizado correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="404">Moderador no encontrado.</response>
        /// <remarks>
        /// Este endpoint permite a los administradores actualizar los datos de un moderador.
        /// </remarks>  
        [HttpPut("{id}")]
        public ActionResult Update(int id,[FromBody]UpdateModeratorDTO updateModerator)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool updated = _moderatorService.UpdateModerator(updateModerator, id);
            if(updated == true)
            {
                return NoContent();
            }
            return NotFound();
        }


        /// <summary>
        /// Elimina un moderador.
        /// </summary>
        /// <param name="id">ID del moderador a eliminar.</param>
        /// <returns>Respuesta exitosa si el moderador fue eliminado correctamente.</returns>
        /// <response code="200">Moderador eliminado correctamente.</response>
        /// <response code="404">Moderador no encontrado.</response>
        /// <remarks>
        /// Este endpoint permite a los administradores eliminar moderadores.
        /// </remarks>
        [HttpDelete("{id}")]
        public ActionResult DeleteModerator([FromRoute]int id)
        {
            _moderatorService.DeleteModerator(id);
            return Ok();
        }
    }
}

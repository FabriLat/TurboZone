using Application.Interfaces;
using Application.Models.Requests.SysAdmins;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(Policy = "SysAdminOnly")]
    public class SysAdminController : ControllerBase
    {
        private readonly ISysAdminService _sysAdminService;
        public SysAdminController(ISysAdminService sysAdminService)
        {
            _sysAdminService = sysAdminService;
        }



        /// <summary>
        /// Crea un nuevo SysAdmin.
        /// </summary>
        /// <param name="createSysAdminDTO">Objeto con los datos necesarios para crear un SysAdmin.</param>
        /// <returns>El SysAdmin creado si la operación es exitosa.</returns>
        /// <response code="201">SysAdmin creado correctamente.</response>
        /// <response code="400">Datos inválidos o no se pudo crear el SysAdmin.</response>
        /// <remarks>
        /// Este endpoint requiere que el usuario sea un SysAdmin. Solo los SysAdmins pueden crear otros SysAdmins.
        /// </remarks>
        [HttpPost]
       public ActionResult CreateSysAdmin([FromBody] CreateSysAdminDTO createSysAdminDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SysAdminDTO? created = _sysAdminService.CreateSysAdmin(createSysAdminDTO);
            if (created != null)
            {
                return CreatedAtAction("Get", "User", new { id = created.Id }, created);
            }
            return BadRequest();
        }

        /// <summary>
        /// Actualiza los datos de un SysAdmin existente.
        /// </summary>
        /// <param name="id">ID del SysAdmin a actualizar.</param>
        /// <param name="updateSysAdminDTO">Objeto con los nuevos datos del SysAdmin.</param>
        /// <returns>Sin contenido si la actualización fue exitosa.</returns>
        /// <response code="204">SysAdmin actualizado correctamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint requiere que el usuario sea un SysAdmin. Solo los SysAdmins pueden actualizar sus datos.
        /// </remarks>
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] UpdateSysAdminDTO updateSysAdminDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool updated = _sysAdminService.UpdateSysAdmin(updateSysAdminDTO, id);
            if(updated == true)
            {
                return NoContent();
            }
            return Unauthorized();

        }

        /// <summary>
        /// Obtiene la lista de todos los SysAdmins.
        /// </summary>
        /// <returns>Lista de SysAdmins registrados.</returns>
        /// <response code="200">Lista de SysAdmins obtenida correctamente.</response>
        /// <response code="401">No autorizado: se requiere un token JWT válido.</response>
        /// <remarks>
        /// Este endpoint requiere autenticación JWT. Solo los SysAdmins pueden acceder a esta información.
        /// </remarks>
        [HttpGet]
        public List<SysAdminDTO> GetAllSysAdmin()
        {
            return _sysAdminService.GetAll();
        }

    }
}

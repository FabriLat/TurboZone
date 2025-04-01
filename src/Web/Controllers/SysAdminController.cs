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


        [HttpGet]
        public List<SysAdminDTO> GetAllSysAdmin()
        {
            return _sysAdminService.GetAll();
        }

    }
}

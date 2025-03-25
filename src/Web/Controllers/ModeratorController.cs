using Application.Interfaces;
using Application.Models.Requests;
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

        [HttpPost]
        public ActionResult CreateModerator([FromBody] CreateModeratorDTO createModerator)
        {
           var created = _moderatorService.CreateModerator(createModerator);
            if(created != null)
            {
                return Ok();
            }
            return BadRequest();
            
        }


        [HttpGet]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<ModeratorDTO> GetAll()
        {
            return _moderatorService.GetAllModerators();
        }



        [HttpPut("{id}")]
        public ActionResult Update(int id,[FromBody]UpdateModeratorDTO updateModerator)
        {
            bool updated = _moderatorService.UpdateModerator(updateModerator, id);
            if(updated == true)
            {
                return NoContent();
            }
            return NotFound();
            
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteModerator([FromRoute]int id)
        {
            _moderatorService.DeleteModerator(id);
            return Ok();
        }

    }
}

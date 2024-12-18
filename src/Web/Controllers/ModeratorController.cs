using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly IModeratorService _moderatorService;
        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        [HttpPost("[action]")]
        public ActionResult CreateModerator([FromBody] CreateModeratorDTO createModerator)
        {
            _moderatorService.CreateModerator(createModerator);
            return Ok();
        }


        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<ModeratorDTO> GetAllModerators()
        {
            return _moderatorService.GetAllModerators();
        }



        [HttpPut("[action]")]
        public ActionResult UpdateModerator([FromBody]UpdateModeratorDTO updateModerator)
        {
            _moderatorService.UpdateModerator(updateModerator);
            return Ok();
        }


        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteModerator([FromRoute]int id)
        {
            _moderatorService.DeleteModerator(id);
            return Ok();
        }

    }
}

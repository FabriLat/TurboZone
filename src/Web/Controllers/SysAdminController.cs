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
    [Authorize(Policy = "SysAdminOnly")]
    public class SysAdminController : ControllerBase
    {
        private readonly ISysAdminService _sysAdminService;
        public SysAdminController(ISysAdminService sysAdminService)
        {
            _sysAdminService = sysAdminService;
        }

        [HttpPost]
       public  ActionResult CreateSysAdmin([FromBody] CreateSysAdminDTO createSysAdminDTO)
        {
            _sysAdminService.CreateSysAdmin(createSysAdminDTO);
            return Ok();
        }


        [HttpGet]
        public List<SysAdminDTO> GetAllSysAdmin()
        {
            return _sysAdminService.GetAll();
        }

    }
}

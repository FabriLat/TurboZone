﻿using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysAdminController : ControllerBase
    {
        private readonly ISysAdminService _sysAdminService;
        public SysAdminController(ISysAdminService sysAdminService)
        {
            _sysAdminService = sysAdminService;
        }

        [HttpPost("[action]")]
       public  ActionResult CreateSysAdmin([FromBody] CreateSysAdminDTO createSysAdminDTO)
        {
            _sysAdminService.CreateSysAdmin(createSysAdminDTO);
            return Ok();
        }


        [HttpGet("[action]")]
        public List<SysAdminDTO> GetAllSysAdmin()
        {
            return _sysAdminService.GetAll();
        }

    }
}

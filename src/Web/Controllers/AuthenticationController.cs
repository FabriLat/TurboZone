﻿using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        private readonly IConfiguration _config;

        public AuthenticationController(IAuthenticationService authenticationService, IConfiguration config)
        {
            _authenticationService = authenticationService;
            _config = config;
        }

        [HttpPost]
        public ActionResult<string> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                //Llama a un metodo que devuelve un string-Token
                string token = _authenticationService.Authenticate(authenticationRequest);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

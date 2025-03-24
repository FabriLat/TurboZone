using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "AnonymousOnly")]
        public ActionResult CreateClient([FromBody]CreateClientDTO createClientDTO)
        {
            try
            {
                var created = _clientService.CreateNewClient(createClientDTO);
                if(created != null)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<ClientDTO?> GetClientById([FromRoute]int id)
        {
            ClientDTO? client = _clientService.GetClientById(id);
            if (client != null)
            {
                return client;
            }
            return NotFound();
        }


        [HttpGet("[action]")]
        public List<ClientDTO> GetAllClients()
        {
            return _clientService.GetAllClients();
        }

        [HttpPut("[action]/{id}")]
        public ActionResult UpdateClient(int id, [FromBody] UpdateClientDTO updateClientDTO)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                bool updated = _clientService.UpdateClient(updateClientDTO, id, userId);
                if(updated) 
                {
                    return Ok();
                }
                else
                {
                    return NotFound("Client not found.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpDelete("[action]/{id}")]
        [Authorize]
        public ActionResult DeleteClient([FromRoute] int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var deletedClient = _clientService.DeleteClient(id, userId);
            if (deletedClient != null) 
            {
                return Ok();
            }
            return Unauthorized();
        }

    }
}

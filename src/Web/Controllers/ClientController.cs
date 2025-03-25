using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [Authorize(Policy = "AnonymousOnly")]
        public ActionResult Create([FromBody] CreateClientDTO createClientDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _clientService.CreateNewClient(createClientDTO);
            if (created == null)
            {
                return BadRequest(new { Message = "No se pudo crear el cliente" });
            }

            return CreatedAtAction("Get","User" , new { id = created.Id }, created);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ClientDTO>> GetAll()
        {
            var clients = _clientService.GetAllClients();
            return Ok(clients);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Update(int id, [FromBody] UpdateClientDTO updateClientDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            bool updated = _clientService.UpdateClient(updateClientDTO, id, userId);
            if (!updated)
            {
                return NotFound(new { Message = "Cliente no encontrado o no autorizado" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "Usuario no identificado" });
            }

            int userId = int.Parse(userIdClaim.Value);
            var deletedClient = _clientService.DeleteClient(id, userId);
            if (deletedClient == null)
            {
                return Forbid();
            }

            return NoContent();
        }
    }
}
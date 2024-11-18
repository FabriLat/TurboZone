using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult CreateClient([FromBody]CreateClientDTO createClientDTO)
        {
            try
            {
                return Ok(_clientService.CreateNewClient(createClientDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        [HttpGet("[action]")]
        public List<ClientDTO> GetAllClients()
        {
            return _clientService.GetAllClients();
        }


        [HttpPut("[action]")]
        public ActionResult UpdateClient([FromBody] UpdateClientDTO updateClientDTO)
        {
            try
            {
                _clientService.UpdateClient(updateClientDTO);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}

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
            return Ok(_clientService.CreateNewClient(createClientDTO));
        }


        [HttpGet("[action]")]
        public List<ClientDTO> GetAllClients()
        {
            return _clientService.GetAllClients();
        }

    }
}

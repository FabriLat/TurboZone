using Application.Interfaces;
using Application.Models.Requests.Vehicles;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateVehicle([FromBody] CreateVehicleDTO createVehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var create = _vehicleService.CreateVehicle(createVehicle, userId);
            if(create != null)
            {
                return CreatedAtAction(nameof(Get), new { id = create.Id }, create);
            }
            return BadRequest();  
        }


        [HttpGet]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetAll()
        {
            return _vehicleService.GetAllVehicles();
        }


        [HttpGet("{id}")]
        public VehicleDTO? Get([FromRoute]int id)
        {
                VehicleDTO? vehicleDto =  _vehicleService.GetVehicleById(id);
                if (vehicleDto != null)
                {
                    return vehicleDto;
                }
                return null;
        }


        [HttpGet("[action]")]
        public List<VehicleDTO> GetActive()
        {
            return _vehicleService.GetActiveVehicles();
        }


        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetPending()
        {
            return _vehicleService.GetPendingVehicles();
        }


        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetPendingUpdate()
        {
            
            return _vehicleService.GetPendingUpdateVehicles();
        }


        [HttpPut("{id}")]
        [Authorize]
        public ActionResult? Update(int id,[FromBody]UpdateVehicleDTO updateVehicle)
        {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var vehicle = _vehicleService.UpdateVehicle(updateVehicle, userId, id);
                if (vehicle != null)
                {
                    return Ok();
                }
                return Unauthorized();
        }


        [HttpPut("changeState/{id}")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public ActionResult? ChangeState(int id, [FromBody]string newState)
        {
                if(_vehicleService.ChangeVehicleState(id, newState))
                {
                    return Ok();
                }
                return NotFound();
        }
    }
}
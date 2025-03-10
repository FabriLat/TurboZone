using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [HttpPost("[action]")]
        [Authorize]
        public ActionResult CreateVehicle([FromBody] CreateVehicleDTO createVehicle)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            var create = _vehicleService.CreateVehicle(createVehicle, userId);
            if(create == true)
            {
                return Ok();
            }
            return BadRequest();
            
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetAllVehicles()
        {
            return _vehicleService.GetAllVehicles();
        }

        [HttpGet("[action]/{id}")]
        public VehicleDTO? GetVehicleById([FromRoute]int id)
        {
            try
            {
                VehicleDTO vehicleDto =  _vehicleService.GetVehicleById(id);
                if (vehicleDto != null)
                {
                    return vehicleDto;
                }
                return null;
            }catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet("[action]")]
        public List<VehicleDTO> GetActiveVehicles()
        {
            return _vehicleService.GetActiveVehicles();
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetPendingVehicles()
        {
            return _vehicleService.GetPendingVehicles();
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetPendingUpdateVehicles()
        {
            
            return _vehicleService.GetPendingUpdateVehicles();
        }



        [HttpPut("[action]/{vehicleId}")]
        [Authorize]
        public ActionResult? UpdateVehicle(int vehicleId,[FromBody]UpdateVehicleDTO updateVehicle)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var vehicle = _vehicleService.UpdateVehicle(updateVehicle, userId, vehicleId);
                if (vehicle != null)
                {
                    return Ok();
                }
                return Unauthorized();
            }catch (Exception ex)
            {
                return BadRequest();
            }
          
        }

        [HttpPut("[action]/{vehicleId}")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public ActionResult? ChangeVehicleState(int vehicleId, [FromBody]string newState)
        {
            try
            {
                if(_vehicleService.ChangeVehicleState(vehicleId, newState))
                {
                    return Ok();
                }
                return NotFound();
                
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

    }
}
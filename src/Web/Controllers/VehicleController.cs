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
            _vehicleService.CreateVehicle(createVehicle, userId);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "ModeratorAndSysAdmin")]
        public List<VehicleDTO> GetAllVehicles()
        {
            return _vehicleService.GetAllVehicles();
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


        [HttpPut("[action]")]
        [Authorize]
        public ActionResult? UpdateVehicle(UpdateVehicleDTO updateVehicle)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
           var vehicle = _vehicleService.UpdateVehicle(updateVehicle, userId);
            if (vehicle != null)
            {
                return Ok();
            }
            return Unauthorized();
        }

    }
}
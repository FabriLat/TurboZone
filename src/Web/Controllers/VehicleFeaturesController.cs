using Application.Interfaces;
using Application.Models.Requests.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class VehicleFeatureController : ControllerBase
    {
        private readonly IVehicleFeatureService _vehicleFeatureService;
        public VehicleFeatureController(IVehicleFeatureService vehicleFeatureService)
        {
            _vehicleFeatureService = vehicleFeatureService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddFeaturesToVehicle(FeatureDTO featureDTO)
        {


            return Ok();
        }

    }
}

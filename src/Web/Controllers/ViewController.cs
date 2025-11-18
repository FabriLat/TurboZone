using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ViewController : ControllerBase
    {

        private readonly IViewService _viewService;
        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }

        [HttpPost("{vehicleId}")]
        public ActionResult ViewVehicle(int vehicleId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _viewService.LogView(vehicleId, null);
                return NoContent();
            }
            _viewService.LogView(vehicleId, int.Parse(userId));
            return NoContent();
        }

        [HttpGet("{vehicleId}")]
        public int Get(int vehicleId)
        {
            int totalViews = _viewService.GetTotalViewsByVehicle(vehicleId);
            return totalViews;
        }
    }
}

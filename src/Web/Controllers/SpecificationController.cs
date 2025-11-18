using Application.Interfaces;
using Application.Models.Requests.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        private readonly ISpecificationService _specificationService;
        public SpecificationController(ISpecificationService specificationService)
        {
            _specificationService = specificationService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateSpecification(SpecificationDTO specificationDTO)
        {
            var result = _specificationService.AddSpecification(specificationDTO);
            if (!result)
            {
                return BadRequest("Could not create specification. Vehicle may not exist.");
            }
            return Ok("Specification created successfully.");
        }


        [HttpGet("vehicle/{vehicleId}")]
        public IActionResult GetSpecificationByVehicleId(int vehicleId)
        {
            var specificationDTO = _specificationService.GetSpecificationByVehicleId(vehicleId);
            if (specificationDTO == null)
            {
                return NotFound("Specification not found for the given vehicle ID.");
            }
            return Ok(specificationDTO);
        }
    }
}

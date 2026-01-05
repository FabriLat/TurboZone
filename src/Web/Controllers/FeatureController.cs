using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }



        /// <summary>
        /// Obtiene la lista de features disponibles.
        /// </summary>
        /// <returns>Listado de features.</returns>
        /// <response code="200">Lista de features obtenida correctamente.</response>
        /// <remarks>
        /// Este endpoint devuelve las features previamente cargadas.
        /// </remarks>
        [HttpGet]
        public ActionResult GetAllFeatures()
        {
            var features = _featureService.GetAllFeatures();
            return Ok(features);
        }

    }


}

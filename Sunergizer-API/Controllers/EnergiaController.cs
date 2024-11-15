using Microsoft.AspNetCore.Mvc;
using Sunergizer_API.Services;

namespace Sunergizer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiaController : ControllerBase
    {
        private readonly EnergiaPredictionService _energiaPredictionService;

        public EnergiaController(EnergiaPredictionService energiaPredictionService)
        {
            _energiaPredictionService = energiaPredictionService;
        }

        [HttpGet("predict")]
        public IActionResult Predict([FromQuery] double kwhConsumidos)
        {
            // Converte de double para float
            var suggestion = _energiaPredictionService.Predict((float)kwhConsumidos);
            return Ok(new { Sugestao = suggestion });
        }
    }
}

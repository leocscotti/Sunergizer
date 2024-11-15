using Microsoft.AspNetCore.Mvc;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;

namespace Sunergizer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FontesEnergiaController : ControllerBase
    {
        private readonly IFonteEnergiaService _fonteEnergiaService;

        public FontesEnergiaController(IFonteEnergiaService fonteEnergiaService)
        {
            _fonteEnergiaService = fonteEnergiaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FonteEnergia>>> GetAllFontesEnergia()
        {
            return Ok(await _fonteEnergiaService.GetAllFontesEnergiaAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FonteEnergia>> GetFonteEnergiaById(int id)
        {
            var fonteEnergia = await _fonteEnergiaService.GetFonteEnergiaByIdAsync(id);
            if (fonteEnergia == null)
                return NotFound();

            return Ok(fonteEnergia);
        }

        [HttpPost]
        public async Task<ActionResult<FonteEnergia>> AddFonteEnergia([FromBody] FonteEnergiaRequest fonteEnergiaRequest)
        {
            var fonteEnergia = await _fonteEnergiaService.AddFonteEnergiaAsync(fonteEnergiaRequest);
            return CreatedAtAction(nameof(GetFonteEnergiaById), new { id = fonteEnergia.Id }, fonteEnergia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFonteEnergia(int id, [FromBody] FonteEnergiaRequest fonteEnergiaRequest)
        {
            var updatedFonteEnergia = await _fonteEnergiaService.UpdateFonteEnergiaAsync(id, fonteEnergiaRequest);
            if (updatedFonteEnergia == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFonteEnergia(int id)
        {
            var deleted = await _fonteEnergiaService.DeleteFonteEnergiaAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

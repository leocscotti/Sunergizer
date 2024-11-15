using Microsoft.AspNetCore.Mvc;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;

namespace Sunergizer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunidadesController : ControllerBase
    {
        private readonly IComunidadeService _comunidadeService;

        public ComunidadesController(IComunidadeService comunidadeService)
        {
            _comunidadeService = comunidadeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comunidade>>> GetAllComunidades()
        {
            return Ok(await _comunidadeService.GetAllComunidadesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comunidade>> GetComunidadeById(int id)
        {
            var comunidade = await _comunidadeService.GetComunidadeByIdAsync(id);
            if (comunidade == null)
                return NotFound();

            return Ok(comunidade);
        }

        [HttpPost]
        public async Task<ActionResult<Comunidade>> AddComunidade([FromBody] ComunidadeRequest comunidadeRequest)
        {
            var comunidade = await _comunidadeService.AddComunidadeAsync(comunidadeRequest);
            return CreatedAtAction(nameof(GetComunidadeById), new { id = comunidade.Id }, comunidade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComunidade(int id, [FromBody] ComunidadeRequest comunidadeRequest)
        {
            var updatedComunidade = await _comunidadeService.UpdateComunidadeAsync(id, comunidadeRequest);
            if (updatedComunidade == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComunidade(int id)
        {
            var deleted = await _comunidadeService.DeleteComunidadeAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

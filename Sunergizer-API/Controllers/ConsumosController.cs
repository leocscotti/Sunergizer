using Microsoft.AspNetCore.Mvc;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;

namespace Sunergizer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumosController : ControllerBase
    {
        private readonly IConsumoService _consumoService;

        public ConsumosController(IConsumoService consumoService)
        {
            _consumoService = consumoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumo>>> GetAllConsumos()
        {
            return Ok(await _consumoService.GetAllConsumosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Consumo>> GetConsumoById(int id)
        {
            var consumo = await _consumoService.GetConsumoByIdAsync(id);
            if (consumo == null)
                return NotFound();

            return Ok(consumo);
        }

        [HttpPost]
        public async Task<ActionResult<Consumo>> AddConsumo([FromBody] ConsumoRequest consumoRequest)
        {
            var consumo = await _consumoService.AddConsumoAsync(consumoRequest);
            return CreatedAtAction(nameof(GetConsumoById), new { id = consumo.Id }, consumo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumo(int id, [FromBody] ConsumoRequest consumoRequest)
        {
            var updatedConsumo = await _consumoService.UpdateConsumoAsync(id, consumoRequest);
            if (updatedConsumo == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumo(int id)
        {
            var deleted = await _consumoService.DeleteConsumoAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

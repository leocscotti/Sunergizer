using Microsoft.AspNetCore.Mvc;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunergizer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> AddUsuario([FromBody] UsuarioRequest usuarioRequest)
        {
            var newUserId = await _usuarioService.AddUsuarioAsync(usuarioRequest);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = newUserId }, usuarioRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuario(int id, [FromBody] UsuarioRequest usuarioRequest)
        {
            var existingUsuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (existingUsuario == null)
                return NotFound();

            await _usuarioService.UpdateUsuarioAsync(id, usuarioRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var existingUsuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (existingUsuario == null)
                return NotFound();

            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent();
        }
    }
}

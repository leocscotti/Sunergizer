using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;

namespace Sunergizer_API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SunergizerDBContext _dbContext;

        public UsuarioService(SunergizerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }

        public async Task<int> AddUsuarioAsync(UsuarioRequest usuarioRequest)
        {
            var usuario = new Usuario
            {
                Nome = usuarioRequest.Nome,
                Endereco = usuarioRequest.Endereco,
                Email = usuarioRequest.Email
            };

            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario.Id;
        }

        public async Task UpdateUsuarioAsync(int id, UsuarioRequest usuarioRequest)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                usuario.Nome = usuarioRequest.Nome;
                usuario.Endereco = usuarioRequest.Endereco;
                usuario.Email = usuarioRequest.Email;

                _dbContext.Usuarios.Update(usuario);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _dbContext.Usuarios.Remove(usuario);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

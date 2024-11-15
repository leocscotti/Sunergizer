using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunergizer_API.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<int> AddUsuarioAsync(UsuarioRequest usuarioRequest);
        Task UpdateUsuarioAsync(int id, UsuarioRequest usuarioRequest);
        Task DeleteUsuarioAsync(int id);
    }
}

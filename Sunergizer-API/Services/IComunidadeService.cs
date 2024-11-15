using Sunergizer_API.DTO;
using Sunergizer_API.Models;

namespace Sunergizer_API.Services
{
    public interface IComunidadeService
    {
        Task<IEnumerable<Comunidade>> GetAllComunidadesAsync();
        Task<Comunidade?> GetComunidadeByIdAsync(int id);
        Task<Comunidade> AddComunidadeAsync(ComunidadeRequest comunidadeRequest);
        Task<Comunidade?> UpdateComunidadeAsync(int id, ComunidadeRequest comunidadeRequest);
        Task<bool> DeleteComunidadeAsync(int id);
    }
}

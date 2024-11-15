using Sunergizer_API.DTO;
using Sunergizer_API.Models;

namespace Sunergizer_API.Services
{
    public interface IConsumoService
    {
        Task<IEnumerable<Consumo>> GetAllConsumosAsync();
        Task<Consumo?> GetConsumoByIdAsync(int id);
        Task<Consumo> AddConsumoAsync(ConsumoRequest consumoRequest);
        Task<Consumo?> UpdateConsumoAsync(int id, ConsumoRequest consumoRequest);
        Task<bool> DeleteConsumoAsync(int id);
    }
}

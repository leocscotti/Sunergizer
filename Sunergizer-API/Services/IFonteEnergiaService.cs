using Sunergizer_API.DTO;
using Sunergizer_API.Models;

namespace Sunergizer_API.Services
{
    public interface IFonteEnergiaService
    {
        Task<IEnumerable<FonteEnergia>> GetAllFontesEnergiaAsync();
        Task<FonteEnergia?> GetFonteEnergiaByIdAsync(int id);
        Task<FonteEnergia> AddFonteEnergiaAsync(FonteEnergiaRequest fonteEnergiaRequest);
        Task<FonteEnergia?> UpdateFonteEnergiaAsync(int id, FonteEnergiaRequest fonteEnergiaRequest);
        Task<bool> DeleteFonteEnergiaAsync(int id);
    }
}

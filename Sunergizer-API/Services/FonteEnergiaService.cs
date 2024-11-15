using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;

namespace Sunergizer_API.Services
{
    public class FonteEnergiaService : IFonteEnergiaService
    {
        private readonly SunergizerDBContext _context;

        public FonteEnergiaService(SunergizerDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FonteEnergia>> GetAllFontesEnergiaAsync()
        {
            return await _context.FontesEnergia.ToListAsync();
        }

        public async Task<FonteEnergia?> GetFonteEnergiaByIdAsync(int id)
        {
            return await _context.FontesEnergia.FindAsync(id);
        }

        public async Task<FonteEnergia> AddFonteEnergiaAsync(FonteEnergiaRequest fonteEnergiaRequest)
        {
            var fonteEnergia = new FonteEnergia
            {
                Tipo = fonteEnergiaRequest.Tipo,
                Descricao = fonteEnergiaRequest.Descricao
            };
            _context.FontesEnergia.Add(fonteEnergia);
            await _context.SaveChangesAsync();
            return fonteEnergia;
        }

        public async Task<FonteEnergia?> UpdateFonteEnergiaAsync(int id, FonteEnergiaRequest fonteEnergiaRequest)
        {
            var fonteEnergia = await _context.FontesEnergia.FindAsync(id);
            if (fonteEnergia != null)
            {
                fonteEnergia.Tipo = fonteEnergiaRequest.Tipo;
                fonteEnergia.Descricao = fonteEnergiaRequest.Descricao;

                await _context.SaveChangesAsync();
            }
            return fonteEnergia;
        }

        public async Task<bool> DeleteFonteEnergiaAsync(int id)
        {
            var fonteEnergia = await _context.FontesEnergia.FindAsync(id);
            if (fonteEnergia != null)
            {
                _context.FontesEnergia.Remove(fonteEnergia);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

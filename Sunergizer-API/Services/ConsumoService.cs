using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;

namespace Sunergizer_API.Services
{
    public class ConsumoService : IConsumoService
    {
        private readonly SunergizerDBContext _context;

        public ConsumoService(SunergizerDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consumo>> GetAllConsumosAsync()
        {
            return await _context.Consumos.ToListAsync();
        }

        public async Task<Consumo?> GetConsumoByIdAsync(int id)
        {
            return await _context.Consumos.FindAsync(id);
        }

        public async Task<Consumo> AddConsumoAsync(ConsumoRequest consumoRequest)
        {
            var consumo = new Consumo
            {
                IdUsuario = consumoRequest.IdUsuario,
                IdFonte = consumoRequest.IdFonte,
                DataRegistro = consumoRequest.DataRegistro,
                KwhConsumidos = consumoRequest.KwhConsumidos
            };
            _context.Consumos.Add(consumo);
            await _context.SaveChangesAsync();
            return consumo;
        }

        public async Task<Consumo?> UpdateConsumoAsync(int id, ConsumoRequest consumoRequest)
        {
            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo != null)
            {
                consumo.IdUsuario = consumoRequest.IdUsuario;
                consumo.IdFonte = consumoRequest.IdFonte;
                consumo.DataRegistro = consumoRequest.DataRegistro;
                consumo.KwhConsumidos = consumoRequest.KwhConsumidos;
                await _context.SaveChangesAsync();
            }
            return consumo;
        }

        public async Task<bool> DeleteConsumoAsync(int id)
        {
            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo != null)
            {
                _context.Consumos.Remove(consumo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;

namespace Sunergizer_API.Services
{
    public class ComunidadeService : IComunidadeService
    {
        private readonly SunergizerDBContext _context;

        public ComunidadeService(SunergizerDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comunidade>> GetAllComunidadesAsync()
        {
            return await _context.Comunidades.ToListAsync();
        }

        public async Task<Comunidade?> GetComunidadeByIdAsync(int id)
        {
            return await _context.Comunidades.FindAsync(id);
        }

        public async Task<Comunidade> AddComunidadeAsync(ComunidadeRequest comunidadeRequest)
        {
            var comunidade = new Comunidade
            {
                Nome = comunidadeRequest.Nome,
                Cidade = comunidadeRequest.Cidade,
                Uf = comunidadeRequest.Uf,
                TotalUsuarios = comunidadeRequest.TotalUsuarios
            };
            _context.Comunidades.Add(comunidade);
            await _context.SaveChangesAsync();
            return comunidade;
        }

        public async Task<Comunidade?> UpdateComunidadeAsync(int id, ComunidadeRequest comunidadeRequest)
        {
            var comunidade = await _context.Comunidades.FindAsync(id);
            if (comunidade != null)
            {
                comunidade.Nome = comunidadeRequest.Nome;
                comunidade.Cidade = comunidadeRequest.Cidade;
                comunidade.Uf = comunidadeRequest.Uf;
                comunidade.TotalUsuarios = comunidadeRequest.TotalUsuarios;
                await _context.SaveChangesAsync();
            }
            return comunidade;
        }

        public async Task<bool> DeleteComunidadeAsync(int id)
        {
            var comunidade = await _context.Comunidades.FindAsync(id);
            if (comunidade != null)
            {
                _context.Comunidades.Remove(comunidade);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

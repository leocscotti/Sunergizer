using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Mapping;
using Sunergizer_API.Models;

namespace Sunergizer_API.Database
{
    public class SunergizerDBContext(DbContextOptions<SunergizerDBContext> options): DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<FonteEnergia> FontesEnergia { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<Comunidade> Comunidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new FonteEnergiaMapping());
            modelBuilder.ApplyConfiguration(new ConsumoMapping());
            modelBuilder.ApplyConfiguration(new ComunidadeMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}

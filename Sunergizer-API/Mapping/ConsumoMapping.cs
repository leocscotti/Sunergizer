using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Models;

namespace Sunergizer_API.Mapping
{
    public class ConsumoMapping : IEntityTypeConfiguration<Consumo>
    {
        public void Configure(EntityTypeBuilder<Consumo> builder)
        {
            builder.ToTable("Consumos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("ID")
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.DataRegistro)
                   .HasColumnName("DATA_REGISTRO")
                   .IsRequired();

            builder.Property(c => c.KwhConsumidos)
                   .HasColumnName("KWH_CONSUMIDOS")
                   .IsRequired();

            builder.HasOne(c => c.Usuario)
                   .WithMany()
                   .HasForeignKey(c => c.IdUsuario)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.FonteEnergia)
                   .WithMany()
                   .HasForeignKey(c => c.IdFonte)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

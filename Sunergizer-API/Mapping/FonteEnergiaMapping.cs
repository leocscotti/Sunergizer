using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Models;

namespace Sunergizer_API.Mapping
{
    public class FonteEnergiaMapping : IEntityTypeConfiguration<FonteEnergia>
    {
        public void Configure(EntityTypeBuilder<FonteEnergia> builder)
        {
            builder.ToTable("FontesEnergia");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                   .HasColumnName("ID")
                   .ValueGeneratedOnAdd();

            builder.Property(f => f.Tipo)
                   .HasColumnName("TIPO")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(f => f.Descricao)
                   .HasColumnName("DESCRICAO")
                   .HasMaxLength(200);
        }
    }
}

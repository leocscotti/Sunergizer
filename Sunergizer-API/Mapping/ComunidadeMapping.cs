using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Models;

namespace Sunergizer_API.Mapping
{
    public class ComunidadeMapping : IEntityTypeConfiguration<Comunidade>
    {
        public void Configure(EntityTypeBuilder<Comunidade> builder)
        {
            builder.ToTable("Comunidades");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("ID")
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                   .HasColumnName("NOME")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Cidade)
                   .HasColumnName("CIDADE")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Uf)
                   .HasColumnName("UF")
                   .IsRequired()
                   .HasMaxLength(2)
                   .IsFixedLength();

            builder.Property(c => c.TotalUsuarios)
                   .HasColumnName("TOTAL_USUARIOS")
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}

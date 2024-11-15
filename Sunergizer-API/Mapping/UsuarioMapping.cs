using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sunergizer_API.Models;

namespace Sunergizer_API.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("ID")
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Nome)
                   .HasColumnName("NOME")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Endereco)
                   .HasColumnName("ENDERECO")
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.Email)
                   .HasColumnName("EMAIL")
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}

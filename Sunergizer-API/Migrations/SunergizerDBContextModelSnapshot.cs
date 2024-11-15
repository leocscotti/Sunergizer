﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using Sunergizer_API.Database;

#nullable disable

namespace Sunergizer_API.Migrations
{
    [DbContext(typeof(SunergizerDBContext))]
    partial class SunergizerDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sunergizer_API.Models.Comunidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("CIDADE");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("NOME");

                    b.Property<string>("TotalUsuarios")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("TOTAL_USUARIOS");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("NCHAR(2)")
                        .HasColumnName("UF")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Comunidades", (string)null);
                });

            modelBuilder.Entity("Sunergizer_API.Models.Consumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("DATA_REGISTRO");

                    b.Property<int>("IdFonte")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("KwhConsumidos")
                        .HasColumnType("BINARY_DOUBLE")
                        .HasColumnName("KWH_CONSUMIDOS");

                    b.HasKey("Id");

                    b.HasIndex("IdFonte");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Consumos", (string)null);
                });

            modelBuilder.Entity("Sunergizer_API.Models.FonteEnergia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("DESCRICAO");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("TIPO");

                    b.HasKey("Id");

                    b.ToTable("FontesEnergia", (string)null);
                });

            modelBuilder.Entity("Sunergizer_API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("ENDERECO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("Sunergizer_API.Models.Consumo", b =>
                {
                    b.HasOne("Sunergizer_API.Models.FonteEnergia", "FonteEnergia")
                        .WithMany()
                        .HasForeignKey("IdFonte")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sunergizer_API.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FonteEnergia");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}

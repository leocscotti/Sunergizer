using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sunergizer_API.Migrations
{
    /// <inheritdoc />
    public partial class Tabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comunidades",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "NCHAR(2)", fixedLength: true, maxLength: 2, nullable: false),
                    TOTAL_USUARIOS = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunidades", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FontesEnergia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TIPO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FontesEnergia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ENDERECO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Consumos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdFonte = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA_REGISTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    KWH_CONSUMIDOS = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Consumos_FontesEnergia_IdFonte",
                        column: x => x.IdFonte,
                        principalTable: "FontesEnergia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consumos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumos_IdFonte",
                table: "Consumos",
                column: "IdFonte");

            migrationBuilder.CreateIndex(
                name: "IX_Consumos_IdUsuario",
                table: "Consumos",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comunidades");

            migrationBuilder.DropTable(
                name: "Consumos");

            migrationBuilder.DropTable(
                name: "FontesEnergia");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}

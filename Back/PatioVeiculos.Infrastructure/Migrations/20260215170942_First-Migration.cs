using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatioVeiculos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrecosHoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrimeiraHora = table.Column<decimal>(type: "TEXT", nullable: false),
                    DemaisHoras = table.Column<decimal>(type: "TEXT", nullable: false),
                    AplicadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecosHoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Placa = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Cor = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataHoraEntrada = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataHoraSaida = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValorCobrado = table.Column<decimal>(type: "TEXT", nullable: true),
                    SessaoAberta = table.Column<bool>(type: "INTEGER", nullable: false),
                    VeiculoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoHorasId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_PrecosHoras_PrecoHorasId",
                        column: x => x.PrecoHorasId,
                        principalTable: "PrecosHoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_PrecoHorasId",
                table: "Movimentacoes",
                column: "PrecoHorasId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_VeiculoId",
                table: "Movimentacoes",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_Placa",
                table: "Veiculos",
                column: "Placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacoes");

            migrationBuilder.DropTable(
                name: "PrecosHoras");

            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}

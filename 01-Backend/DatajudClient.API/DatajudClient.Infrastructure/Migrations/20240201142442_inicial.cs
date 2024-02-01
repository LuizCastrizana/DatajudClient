using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DatajudClient.Infrastructure.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NumeroProcesso = table.Column<string>(type: "longtext", nullable: false),
                    NomeCaso = table.Column<string>(type: "longtext", nullable: false),
                    Vara = table.Column<string>(type: "longtext", nullable: false),
                    Comarca = table.Column<string>(type: "longtext", nullable: false),
                    Estado = table.Column<string>(type: "longtext", nullable: false),
                    Observacao = table.Column<string>(type: "longtext", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AndamentoProcesso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProcessoId = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AndamentoProcesso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AndamentoProcesso_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComplementoAndamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: false),
                    AndamentoProcessoId = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementoAndamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplementoAndamento_AndamentoProcesso_AndamentoProcessoId",
                        column: x => x.AndamentoProcessoId,
                        principalTable: "AndamentoProcesso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AndamentoProcesso_ProcessoId",
                table: "AndamentoProcesso",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoAndamento_AndamentoProcessoId",
                table: "ComplementoAndamento",
                column: "AndamentoProcessoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplementoAndamento");

            migrationBuilder.DropTable(
                name: "AndamentoProcesso");

            migrationBuilder.DropTable(
                name: "Processo");
        }
    }
}

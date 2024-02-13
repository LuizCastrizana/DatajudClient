using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatajudClient.Infrastructure.Migrations
{
    public partial class camposnovos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaAtualizacao",
                table: "Processo",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoAndamento",
                table: "Processo",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UltimaAtualizacao",
                table: "Processo");

            migrationBuilder.DropColumn(
                name: "UltimoAndamento",
                table: "Processo");
        }
    }
}

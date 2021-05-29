using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentAssistant.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Volts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OpeningLiquidMoney = table.Column<double>(type: "REAL", nullable: false),
                    OpeningCashMoney = table.Column<double>(type: "REAL", nullable: false),
                    ClosingCashMoney = table.Column<double>(type: "REAL", nullable: false),
                    ClosingLiquidMoney = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volts");
        }
    }
}

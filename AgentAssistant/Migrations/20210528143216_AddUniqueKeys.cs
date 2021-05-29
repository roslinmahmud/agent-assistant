using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentAssistant.Migrations
{
    public partial class AddUniqueKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Volt_AgentId",
                table: "Volt");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Volt_AgentId_Date",
                table: "Volt",
                columns: new[] { "AgentId", "Date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Volt_AgentId_Date",
                table: "Volt");

            migrationBuilder.CreateIndex(
                name: "IX_Volt_AgentId",
                table: "Volt",
                column: "AgentId");
        }
    }
}

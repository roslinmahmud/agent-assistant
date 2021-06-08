using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentAssistant.Migrations
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "012e18c8-0687-4b88-853e-46314c2e3a8c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1701fbc3-be39-4b88-ba41-762a20cc97f2");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Volt",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Agents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Agents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad0bac1a-a059-4db4-a88a-ca39b0ebb2c2", "9c40c304-d968-4577-b23b-914fe65830f9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c0250a37-968f-4b57-8673-ee4e9aec92a2", "9f75c0ca-fa62-4245-9f01-7941db3b83a1", "InCharge", "INCHARGE" });

            migrationBuilder.CreateIndex(
                name: "IX_Volt_AgentId",
                table: "Volt",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AgentId",
                table: "Employees",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Agents_AgentId",
                table: "Employees",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Agents_AgentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt");

            migrationBuilder.DropIndex(
                name: "IX_Volt_AgentId",
                table: "Volt");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AgentId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad0bac1a-a059-4db4-a88a-ca39b0ebb2c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0250a37-968f-4b57-8673-ee4e9aec92a2");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Volt");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Agents");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1701fbc3-be39-4b88-ba41-762a20cc97f2", "e418e358-9a70-4503-afd5-f43e083d5cd5", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "012e18c8-0687-4b88-853e-46314c2e3a8c", "ac781abb-2c55-46e8-b45b-b4731cf32a98", "InCharge", "INCHARGE" });
        }
    }
}

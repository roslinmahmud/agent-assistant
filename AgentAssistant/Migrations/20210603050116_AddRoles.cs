using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentAssistant.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volt",
                table: "Volt");

            migrationBuilder.DropIndex(
                name: "IX_Volt_AgentId",
                table: "Volt");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad0bac1a-a059-4db4-a88a-ca39b0ebb2c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0250a37-968f-4b57-8673-ee4e9aec92a2");

            migrationBuilder.RenameTable(
                name: "Volt",
                newName: "Volts");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Volts_AgentId_Date",
                table: "Volts",
                columns: new[] { "AgentId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volts",
                table: "Volts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff970d83-f67c-4514-a3fc-a84179edea91", "0ec190be-f3c9-4c12-8fcf-711a51314126", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb8470cb-c3ae-4bce-a916-01688efe9efc", "f8f9cb04-5af7-4305-8c17-9093e4bafdd3", "Agent", "AGENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1957c156-e3b2-46de-9e2c-32c5fd3654e0", "1bd47fc3-678a-409c-94a9-843eb866195f", "InCharge", "INCHARGE" });

            migrationBuilder.AddForeignKey(
                name: "FK_Volts_Agents_AgentId",
                table: "Volts",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volts_Agents_AgentId",
                table: "Volts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Volts_AgentId_Date",
                table: "Volts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Volts",
                table: "Volts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1957c156-e3b2-46de-9e2c-32c5fd3654e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb8470cb-c3ae-4bce-a916-01688efe9efc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff970d83-f67c-4514-a3fc-a84179edea91");

            migrationBuilder.RenameTable(
                name: "Volts",
                newName: "Volt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Volt",
                table: "Volt",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

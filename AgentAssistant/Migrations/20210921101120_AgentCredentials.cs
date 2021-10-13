using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentAssistant.Migrations
{
    public partial class AgentCredentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42b01f21-d90c-48f7-a714-490bc0c6de0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75bb2995-4ce1-47ad-8bf9-0d353a83d910");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dca8724f-9ee8-426d-97af-ee21658d0222");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Agents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53206f1b-1d9d-45c8-a1b8-b5429d210025", "a59132d9-ab37-410a-857d-cbf4b61a1a80", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b57083e-a281-4f5b-9230-dfcd4f15ead1", "ef030302-f0b0-40f6-9e30-d8c738ed84ac", "Agent", "AGENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a530b8bb-4343-4626-b2c1-aca46a838b7e", "41cd7971-77f0-40d8-9531-38f037a1f65a", "InCharge", "INCHARGE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b57083e-a281-4f5b-9230-dfcd4f15ead1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53206f1b-1d9d-45c8-a1b8-b5429d210025");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a530b8bb-4343-4626-b2c1-aca46a838b7e");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Agents");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42b01f21-d90c-48f7-a714-490bc0c6de0c", "43aad552-1e45-4b46-b03b-3bdd809407b4", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "75bb2995-4ce1-47ad-8bf9-0d353a83d910", "15ca9e4e-b9a2-4e48-a8c2-c01d985d2690", "Agent", "AGENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dca8724f-9ee8-426d-97af-ee21658d0222", "895308fc-2600-440b-946b-b64e8a169256", "InCharge", "INCHARGE" });
        }
    }
}

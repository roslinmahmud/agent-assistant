using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentAssistant.Migrations
{
    public partial class AddStatement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b1473c9-c72f-4d62-b878-2ecefab442e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "196d57b6-6d9a-447f-ac13-982d96034774");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41a6d709-a285-43a3-b938-59a5e615814c");

            migrationBuilder.CreateTable(
                name: "StatementCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    IsIncome = table.Column<bool>(type: "INTEGER", nullable: false),
                    AgentId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatementCategories_AspNetUsers_AgentId",
                        column: x => x.AgentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    AgentId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statements_AspNetUsers_AgentId",
                        column: x => x.AgentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statements_StatementCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "StatementCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a9264cb-960b-404e-9f68-512a35ecf9b5", "09f23ded-25aa-4eae-a4e7-b82c53bf0021", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c171ab05-7978-4f18-a341-734bfc195bbd", "d35c51e2-199a-45e3-9c3b-e44d1d6a05c7", "Agent", "AGENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba365cc9-dd3c-4a38-ad29-72ca34bc4565", "b5b8e06b-3fd6-4c77-a239-e977b0b8b48e", "InCharge", "INCHARGE" });

            migrationBuilder.CreateIndex(
                name: "IX_StatementCategories_AgentId",
                table: "StatementCategories",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_AgentId",
                table: "Statements",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_CategoryId",
                table: "Statements",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "StatementCategories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a9264cb-960b-404e-9f68-512a35ecf9b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba365cc9-dd3c-4a38-ad29-72ca34bc4565");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c171ab05-7978-4f18-a341-734bfc195bbd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b1473c9-c72f-4d62-b878-2ecefab442e3", "3ed9bd28-0f8a-41b1-a40d-c77cfe25f5d3", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41a6d709-a285-43a3-b938-59a5e615814c", "d88d658d-8270-42e5-b41d-299bb141bafe", "Agent", "AGENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "196d57b6-6d9a-447f-ac13-982d96034774", "c3d336a2-797b-4263-b472-27ff383fc8de", "InCharge", "INCHARGE" });
        }
    }
}

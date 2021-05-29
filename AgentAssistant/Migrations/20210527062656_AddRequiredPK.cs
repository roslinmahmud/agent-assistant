using Microsoft.EntityFrameworkCore.Migrations;

namespace AgentAssistant.Migrations
{
    public partial class AddRequiredPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Agents_AgentId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Agents",
                newName: "AgentId");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Volt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Employee",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Agents_AgentId",
                table: "Employee",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Agents_AgentId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "Agents",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Volt",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Employee",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Agents_AgentId",
                table: "Employee",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Volt_Agents_AgentId",
                table: "Volt",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class Adddulu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId1",
                table: "EmployeeDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeId1",
                table: "EmployeeDetails",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId1",
                table: "EmployeeDetails",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId1",
                table: "EmployeeDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetails_EmployeeId1",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "EmployeeDetails");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

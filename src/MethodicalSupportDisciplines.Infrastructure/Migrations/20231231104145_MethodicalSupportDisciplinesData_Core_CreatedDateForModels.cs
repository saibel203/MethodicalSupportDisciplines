using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Core_CreatedDateForModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DisciplineCreatedDate",
                table: "Disciplines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 31, 12, 41, 43, 343, DateTimeKind.Local).AddTicks(7439));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DisciplineMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 31, 12, 41, 43, 343, DateTimeKind.Local).AddTicks(9443));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisciplineCreatedDate",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DisciplineMaterials");
        }
    }
}

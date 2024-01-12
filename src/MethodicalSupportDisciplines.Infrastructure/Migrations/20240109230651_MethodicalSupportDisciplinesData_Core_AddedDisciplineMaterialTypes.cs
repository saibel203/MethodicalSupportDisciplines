using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Core_AddedDisciplineMaterialTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisciplineMaterialType",
                table: "DisciplineMaterials",
                newName: "DisciplineMaterialTypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisciplineCreatedDate",
                table: "Disciplines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 10, 1, 6, 48, 935, DateTimeKind.Local).AddTicks(1788),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 57, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DisciplineMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 10, 1, 6, 48, 935, DateTimeKind.Local).AddTicks(6026),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 58, DateTimeKind.Local).AddTicks(3369));

            migrationBuilder.CreateTable(
                name: "DisciplineMaterialTypes",
                columns: table => new
                {
                    DisciplineMaterialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineMaterialTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineMaterialTypes", x => x.DisciplineMaterialTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineMaterials_DisciplineMaterialTypeId",
                table: "DisciplineMaterials",
                column: "DisciplineMaterialTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplineMaterials_DisciplineMaterialTypes_DisciplineMaterialTypeId",
                table: "DisciplineMaterials",
                column: "DisciplineMaterialTypeId",
                principalTable: "DisciplineMaterialTypes",
                principalColumn: "DisciplineMaterialTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplineMaterials_DisciplineMaterialTypes_DisciplineMaterialTypeId",
                table: "DisciplineMaterials");

            migrationBuilder.DropTable(
                name: "DisciplineMaterialTypes");

            migrationBuilder.DropIndex(
                name: "IX_DisciplineMaterials_DisciplineMaterialTypeId",
                table: "DisciplineMaterials");

            migrationBuilder.RenameColumn(
                name: "DisciplineMaterialTypeId",
                table: "DisciplineMaterials",
                newName: "DisciplineMaterialType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisciplineCreatedDate",
                table: "Disciplines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 57, DateTimeKind.Local).AddTicks(8064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 10, 1, 6, 48, 935, DateTimeKind.Local).AddTicks(1788));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DisciplineMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 58, DateTimeKind.Local).AddTicks(3369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 10, 1, 6, 48, 935, DateTimeKind.Local).AddTicks(6026));
        }
    }
}

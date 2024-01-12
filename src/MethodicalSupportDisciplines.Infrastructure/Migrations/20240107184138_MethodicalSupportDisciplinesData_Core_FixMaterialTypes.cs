using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Core_FixMaterialTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialBook",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "MaterialUrl",
                table: "Materials");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialPath",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialTypeId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisciplineCreatedDate",
                table: "Disciplines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 57, DateTimeKind.Local).AddTicks(8064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 31, 12, 41, 43, 343, DateTimeKind.Local).AddTicks(7439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DisciplineMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 58, DateTimeKind.Local).AddTicks(3369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 31, 12, 41, 43, 343, DateTimeKind.Local).AddTicks(9443));

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.MaterialTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialTypes_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId",
                principalTable: "MaterialTypes",
                principalColumn: "MaterialTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialTypes_MaterialTypeId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropIndex(
                name: "IX_Materials_MaterialTypeId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "MaterialTypeId",
                table: "Materials");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialPath",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MaterialBook",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialUrl",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DisciplineCreatedDate",
                table: "Disciplines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 31, 12, 41, 43, 343, DateTimeKind.Local).AddTicks(7439),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 57, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DisciplineMaterials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 31, 12, 41, 43, 343, DateTimeKind.Local).AddTicks(9443),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 7, 20, 41, 37, 58, DateTimeKind.Local).AddTicks(3369));
        }
    }
}

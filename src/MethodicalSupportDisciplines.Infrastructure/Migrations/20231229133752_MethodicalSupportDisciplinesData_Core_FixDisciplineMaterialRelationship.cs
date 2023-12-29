using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Core_FixDisciplineMaterialRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplineMaterials_Groups_GroupId",
                table: "DisciplineMaterials");

            migrationBuilder.DropIndex(
                name: "IX_DisciplineMaterials_GroupId",
                table: "DisciplineMaterials");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "DisciplineMaterials",
                newName: "DisciplineMaterialType");

            migrationBuilder.AddColumn<int>(
                name: "DisciplineId",
                table: "DisciplineMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineMaterials_DisciplineId",
                table: "DisciplineMaterials",
                column: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplineMaterials_Disciplines_DisciplineId",
                table: "DisciplineMaterials",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplineMaterials_Disciplines_DisciplineId",
                table: "DisciplineMaterials");

            migrationBuilder.DropIndex(
                name: "IX_DisciplineMaterials_DisciplineId",
                table: "DisciplineMaterials");

            migrationBuilder.DropColumn(
                name: "DisciplineId",
                table: "DisciplineMaterials");

            migrationBuilder.RenameColumn(
                name: "DisciplineMaterialType",
                table: "DisciplineMaterials",
                newName: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineMaterials_GroupId",
                table: "DisciplineMaterials",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplineMaterials_Groups_GroupId",
                table: "DisciplineMaterials",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

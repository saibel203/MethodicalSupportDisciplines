using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Core_AddDisciplineAndMaterialsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisciplineMaterials",
                columns: table => new
                {
                    DisciplineMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineMaterialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisciplineMaterialDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineMaterials", x => x.DisciplineMaterialId);
                    table.ForeignKey(
                        name: "FK_DisciplineMaterials_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialBook = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "MaterialDisciplineMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    DisciplineMaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDisciplineMaterials", x => new { x.MaterialId, x.DisciplineMaterialId });
                    table.ForeignKey(
                        name: "FK_MaterialDisciplineMaterials_DisciplineMaterials_DisciplineMaterialId",
                        column: x => x.DisciplineMaterialId,
                        principalTable: "DisciplineMaterials",
                        principalColumn: "DisciplineMaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialDisciplineMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineMaterials_GroupId",
                table: "DisciplineMaterials",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDisciplineMaterials_DisciplineMaterialId",
                table: "MaterialDisciplineMaterials",
                column: "DisciplineMaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialDisciplineMaterials");

            migrationBuilder.DropTable(
                name: "DisciplineMaterials");

            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}

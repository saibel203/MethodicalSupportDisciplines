using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Core_RenameFacultyTableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialtyName",
                table: "Specialties",
                newName: "SpecialityName");

            migrationBuilder.RenameColumn(
                name: "SpecialtyId",
                table: "Specialties",
                newName: "SpecialityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialityName",
                table: "Specialties",
                newName: "SpecialtyName");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "Specialties",
                newName: "SpecialtyId");
        }
    }
}

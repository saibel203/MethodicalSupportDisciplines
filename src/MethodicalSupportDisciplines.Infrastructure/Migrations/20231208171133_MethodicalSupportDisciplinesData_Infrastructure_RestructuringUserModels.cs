using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Infrastructure_RestructuringUserModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "TeacherUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "StudentUsers");

            migrationBuilder.DropColumn(
                name: "SpecialtyShortName",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "GuestUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AdminUsers");

            migrationBuilder.AddColumn<int>(
                name: "SpecialityNumber",
                table: "Specialties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialityNumber",
                table: "Specialties");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "TeacherUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "StudentUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecialtyShortName",
                table: "Specialties",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "GuestUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Infrastructure_RemoveGuestUnnecessaryProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "GuestUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "GuestUsers");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "GuestUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "GuestUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "GuestUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "GuestUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

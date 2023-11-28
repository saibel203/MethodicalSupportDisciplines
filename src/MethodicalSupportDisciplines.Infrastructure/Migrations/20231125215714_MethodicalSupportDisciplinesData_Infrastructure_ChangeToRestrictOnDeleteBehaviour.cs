using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Infrastructure_ChangeToRestrictOnDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Disciplines_DisciplineId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_StudentUsers_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_TeacherUsers_TeacherId",
                table: "Marks");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Disciplines_DisciplineId",
                table: "Marks",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_StudentUsers_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "StudentUsers",
                principalColumn: "StudentUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_TeacherUsers_TeacherId",
                table: "Marks",
                column: "TeacherId",
                principalTable: "TeacherUsers",
                principalColumn: "TeacherUserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Disciplines_DisciplineId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_StudentUsers_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_TeacherUsers_TeacherId",
                table: "Marks");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Disciplines_DisciplineId",
                table: "Marks",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_StudentUsers_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "StudentUsers",
                principalColumn: "StudentUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_TeacherUsers_TeacherId",
                table: "Marks",
                column: "TeacherId",
                principalTable: "TeacherUsers",
                principalColumn: "TeacherUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

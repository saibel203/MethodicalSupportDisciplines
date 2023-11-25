using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MethodicalSupportDisciplinesData_Infrastructure_AddLearningAndAdditionalDataEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "TeacherUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QualificationId",
                table: "TeacherUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "StudentUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormatLearningId",
                table: "StudentUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "StudentUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LearningStatusId",
                table: "StudentUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "StudentUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "StudentUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.DisciplineId);
                    table.ForeignKey(
                        name: "FK_Disciplines_TeacherUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherUsers",
                        principalColumn: "TeacherUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FacultyShortName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "FormatLearnings",
                columns: table => new
                {
                    FormatLearningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormatLearningName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatLearnings", x => x.FormatLearningId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "LearningStatuses",
                columns: table => new
                {
                    LearningStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearningStatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningStatuses", x => x.LearningStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.QualificationId);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    SpecialtyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialtyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SpecialtyShortName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    MarkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkValue = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.MarkId);
                    table.ForeignKey(
                        name: "FK_Marks_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marks_StudentUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentUsers",
                        principalColumn: "StudentUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marks_TeacherUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherUsers",
                        principalColumn: "TeacherUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineGroups",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineGroups", x => new { x.DisciplineId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_DisciplineGroups_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTeachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeachers", x => new { x.TeacherId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupTeachers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeachers_TeacherUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherUsers",
                        principalColumn: "TeacherUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherUsers_QualificationId",
                table: "TeacherUsers",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_FacultyId",
                table: "StudentUsers",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_FormatLearningId",
                table: "StudentUsers",
                column: "FormatLearningId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_GroupId",
                table: "StudentUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_LearningStatusId",
                table: "StudentUsers",
                column: "LearningStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_SpecialtyId",
                table: "StudentUsers",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineGroups_GroupId",
                table: "DisciplineGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeachers_GroupId",
                table: "GroupTeachers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_DisciplineId",
                table: "Marks",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentId",
                table: "Marks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_TeacherId",
                table: "Marks",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsers_Faculties_FacultyId",
                table: "StudentUsers",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsers_FormatLearnings_FormatLearningId",
                table: "StudentUsers",
                column: "FormatLearningId",
                principalTable: "FormatLearnings",
                principalColumn: "FormatLearningId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsers_Groups_GroupId",
                table: "StudentUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsers_LearningStatuses_LearningStatusId",
                table: "StudentUsers",
                column: "LearningStatusId",
                principalTable: "LearningStatuses",
                principalColumn: "LearningStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsers_Specialties_SpecialtyId",
                table: "StudentUsers",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherUsers_Qualifications_QualificationId",
                table: "TeacherUsers",
                column: "QualificationId",
                principalTable: "Qualifications",
                principalColumn: "QualificationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsers_Faculties_FacultyId",
                table: "StudentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsers_FormatLearnings_FormatLearningId",
                table: "StudentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsers_Groups_GroupId",
                table: "StudentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsers_LearningStatuses_LearningStatusId",
                table: "StudentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsers_Specialties_SpecialtyId",
                table: "StudentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherUsers_Qualifications_QualificationId",
                table: "TeacherUsers");

            migrationBuilder.DropTable(
                name: "DisciplineGroups");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "FormatLearnings");

            migrationBuilder.DropTable(
                name: "GroupTeachers");

            migrationBuilder.DropTable(
                name: "LearningStatuses");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_TeacherUsers_QualificationId",
                table: "TeacherUsers");

            migrationBuilder.DropIndex(
                name: "IX_StudentUsers_FacultyId",
                table: "StudentUsers");

            migrationBuilder.DropIndex(
                name: "IX_StudentUsers_FormatLearningId",
                table: "StudentUsers");

            migrationBuilder.DropIndex(
                name: "IX_StudentUsers_GroupId",
                table: "StudentUsers");

            migrationBuilder.DropIndex(
                name: "IX_StudentUsers_LearningStatusId",
                table: "StudentUsers");

            migrationBuilder.DropIndex(
                name: "IX_StudentUsers_SpecialtyId",
                table: "StudentUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "TeacherUsers");

            migrationBuilder.DropColumn(
                name: "QualificationId",
                table: "TeacherUsers");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "StudentUsers");

            migrationBuilder.DropColumn(
                name: "FormatLearningId",
                table: "StudentUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "StudentUsers");

            migrationBuilder.DropColumn(
                name: "LearningStatusId",
                table: "StudentUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "StudentUsers");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "StudentUsers");
        }
    }
}

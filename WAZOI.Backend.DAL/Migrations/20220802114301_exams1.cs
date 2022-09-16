using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class exams1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Subjects_SubjectId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_SolvedExams_Subjects_SubjectId",
                table: "SolvedExams");

            migrationBuilder.DropTable(
                name: "ExamStudentApplications");

            migrationBuilder.DropIndex(
                name: "IX_SolvedExams_SubjectId",
                table: "SolvedExams");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "SolvedExams");

            migrationBuilder.AddColumn<bool>(
                name: "HasExamAccess",
                table: "ExamStudents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
                table: "Exam",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolvedExams_ExamId",
                table: "SolvedExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamStudents_ExamId",
                table: "ExamStudents",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamStudents_StudentId",
                table: "ExamStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_ExamId",
                table: "ExamQuestions",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Subjects_SubjectId",
                table: "Exam",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Exam_ExamId",
                table: "ExamQuestions",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStudents_Exam_ExamId",
                table: "ExamStudents",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStudents_SubjectStudents_StudentId",
                table: "ExamStudents",
                column: "StudentId",
                principalTable: "SubjectStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolvedExams_Exam_ExamId",
                table: "SolvedExams",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Subjects_SubjectId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Exam_ExamId",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStudents_Exam_ExamId",
                table: "ExamStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStudents_SubjectStudents_StudentId",
                table: "ExamStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_SolvedExams_Exam_ExamId",
                table: "SolvedExams");

            migrationBuilder.DropIndex(
                name: "IX_SolvedExams_ExamId",
                table: "SolvedExams");

            migrationBuilder.DropIndex(
                name: "IX_ExamStudents_ExamId",
                table: "ExamStudents");

            migrationBuilder.DropIndex(
                name: "IX_ExamStudents_StudentId",
                table: "ExamStudents");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestions_ExamId",
                table: "ExamQuestions");

            migrationBuilder.DropColumn(
                name: "HasExamAccess",
                table: "ExamStudents");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "SolvedExams",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
                table: "Exam",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "ExamStudentApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExamId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStudentApplications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolvedExams_SubjectId",
                table: "SolvedExams",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Subjects_SubjectId",
                table: "Exam",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SolvedExams_Subjects_SubjectId",
                table: "SolvedExams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}

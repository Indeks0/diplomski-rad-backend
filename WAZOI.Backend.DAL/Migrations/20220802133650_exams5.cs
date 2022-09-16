using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class exams5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolvedExams_Exam_ExamId",
                table: "SolvedExams");

            migrationBuilder.DropIndex(
                name: "IX_SolvedExams_ExamId",
                table: "SolvedExams");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "SolvedExams",
                newName: "ExamStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedExams_ExamStudentId",
                table: "SolvedExams",
                column: "ExamStudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SolvedExams_ExamStudents_ExamStudentId",
                table: "SolvedExams",
                column: "ExamStudentId",
                principalTable: "ExamStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolvedExams_ExamStudents_ExamStudentId",
                table: "SolvedExams");

            migrationBuilder.DropIndex(
                name: "IX_SolvedExams_ExamStudentId",
                table: "SolvedExams");

            migrationBuilder.RenameColumn(
                name: "ExamStudentId",
                table: "SolvedExams",
                newName: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_SolvedExams_ExamId",
                table: "SolvedExams",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolvedExams_Exam_ExamId",
                table: "SolvedExams",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAZOI.Backend.DAL.Migrations
{
    public partial class exams2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectRequests_SubjectStudents_StudentId",
                table: "StudentSubjectRequests");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentSubjectRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjectRequests_StudentId",
                table: "StudentSubjectRequests",
                newName: "IX_StudentSubjectRequests_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectRequests_AspNetUsers_UserId",
                table: "StudentSubjectRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectRequests_AspNetUsers_UserId",
                table: "StudentSubjectRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StudentSubjectRequests",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjectRequests_UserId",
                table: "StudentSubjectRequests",
                newName: "IX_StudentSubjectRequests_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectRequests_SubjectStudents_StudentId",
                table: "StudentSubjectRequests",
                column: "StudentId",
                principalTable: "SubjectStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
